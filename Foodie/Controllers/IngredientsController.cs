using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IImageRepository _imageRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientsController(IImageService imageService, IImageRepository imageRepository,
        IIngredientRepository ingredientRepository)
    {
        _imageService = imageService;
        _imageRepository = imageRepository;
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] IngredientCreateRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Upload Image
        try
        {
            var file = new FileUploadDto
            {
                File = request.File,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            await _imageService.UploadImageAsync(file);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("file", e.Message);
        }


        var imageModel = new Image
        {
            File = request.File,
            FileName = request.FileName,
            FileExtension = Path.GetExtension(request.File.FileName),
            FileSizeInBytes = request.FileName.Length,
            Description = request.FileDescription
        };
        var image = await _imageRepository.Upload(imageModel);

        var ingredinet = new Ingredient
        {
            Name = request.Name,
            Desccription = request.Desccription,
            ImageId = image.Id,
            Nutrition = request.Nutrition,
            UnitPrice = request.UnitPrice,
        };

        if (Enum.TryParse<PriceUnitType>(request.PriceUnit, true, out PriceUnitType parsedPriceUnit))
        {
            ingredinet.PriceUnit = parsedPriceUnit;
        }

        await _ingredientRepository.Create(ingredinet);
        return Ok(ingredinet);
    }

    [HttpGet]
    [Route("GetIngredientsByName")]
    public async Task<IActionResult> GetIngredientsByName([FromQuery] string name)
    {
        var ingredients = await _ingredientRepository.GetIngredientsByNameAsync(name);
        if (ingredients == null)
        {
            NotFound();
        }

        return Ok(ingredients);
    }
    [HttpGet]
    [Route("GetIngredientsByIds")]
    public async Task<IActionResult> GetIngredientsByIds([FromQuery] List<int> IngredientIds)
    {
        var ingredients = new List<Ingredient>();
        foreach (var id in IngredientIds)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient != null)
            {
                ingredients.Add(ingredient);
            }
        }
      
        return Ok(ingredients);
    }
    
    [HttpGet]
    [Route("GetDefaultIngredientOptions")]
    public async Task<IActionResult> GetDefaultIngredientOptions()
    {
        var defaultIngredientIds = new int[]
        {
            1,2,3,4,5,6,7,8,9,10
        };
        var ingredients = new List<Ingredient>();

        foreach (var id in defaultIngredientIds)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient != null) ingredients.Add(ingredient);
        }

        return Ok(ingredients);
    }
    
}