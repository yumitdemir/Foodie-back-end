using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;

    public RecipesController(IImageService imageService,
        IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository,
        IUserRepository userRepository, ITagRepository tagRepository)
    {
        _imageService = imageService;
        _ingredientRepository = ingredientRepository;
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _tagRepository = tagRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] RecipeCreateRequestDto request)
    {
        // Model Checks
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _userRepository.GetByIdAsync(request.UserId) == null)
        {
            NotFound("User not found");
        }

        TimeSpan preparationTime = new TimeSpan();
        if (request.PreparationTime != null)
        {
            bool isPreparationRightFormat = TimeSpan.TryParse(request.PreparationTime, out preparationTime);
            if (!isPreparationRightFormat)
            {
                ModelState.AddModelError("PreparationTime", "PreparationTime is not in the correct format");
                return BadRequest(ModelState);
            }
        }


        // Upload Image
        Image thumbnailImage = new Image();
        try
        {
            var file = new FileUploadDto
            {
                File = request.File,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            thumbnailImage = await _imageService.UploadImageAsync(file);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("file", e.Message);
            BadRequest(ModelState);
        }

        // ingredients
        var ingredients = new List<Ingredient>();
        foreach (var id in request.IngredientIds)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound("Couldn't find the ingredient");
            }

            ingredients.Add(ingredient);
        }

        // Recipe steps
        var recipeSteps = new List<RecipeStep>();
        foreach (var step in request.RecipeSteps)
        {
            var tempRecipeStep = new RecipeStep
            {
                Description = step.Description,
                Title = step.Title,
                StepNo = step.StepNo,
            };
            recipeSteps.Add(tempRecipeStep);
        }

        // recipe tags 
        var tags = new List<RecipeTag>();
        if (request.TagIds != null)
            foreach (var tagId in request.TagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId);
                if (tag == null)
                {
                    return NotFound("Couldn't find provided tag");
                }

                tags.Add(tag);
            }

        //Recipe Creation
        var recipeModel = new Recipe
        {
            Description = request.Description,
            Title = request.Title,
            CreatedAt = DateTime.Now,
            UserId = request.UserId,
            PreparationTime = preparationTime,
            ServingSize = request.ServingSize,
            ImageId = thumbnailImage.Id,
            Steps = recipeSteps,
            Ingredients = ingredients,
            RecipeTags = tags
        };
        var recipe = await _recipeRepository.Create(recipeModel);


        return Ok(recipe);
    }

    [HttpGet]
    [Route("searchRecipes")]
    public async Task<IActionResult> SearchRecipes([FromQuery] SearchRecipeRequestDto request)
    {
        var recipes = await _recipeRepository.GetRecipesByFilter(request);

        return Ok(recipes);
    }

    [HttpGet]
    [Route("GetRecipeDetails")]
    public async Task<IActionResult> GetRecipeDetails([FromQuery] int id)
    {
        var recipe = await _recipeRepository.GetRecipeById(id);
        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }
}