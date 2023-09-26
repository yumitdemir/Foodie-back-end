using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DataContext _dataContext;
    private readonly IIngredientRepository _ingredientRepository;

    public RecipeRepository(DataContext dataContext, IIngredientRepository ingredientRepository)
    {
        _dataContext = dataContext;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<Recipe> Create(Recipe recipe)
    {
        await _dataContext.Recipes.AddAsync(recipe);
        await _dataContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<List<Recipe?>> GetRecipesByFilter(SearchRecipeRequestDto request)
    {
        var requestIngredients = new List<Ingredient>();
        foreach (var ingredientId in request.IngredientIds)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
            if (ingredient == null)
            {
                throw new Exception("Couldn't find ingredient");
            }

            requestIngredients.Add(ingredient);
        }

        var recipes = await _dataContext.Recipes.Include(r => r.Ingredients).ThenInclude(ri => ri.Image)
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .Include(r => r.CreatedBy)
            .Include(r => r.ThumbnailImage)
            .Include(r => r.RecipeTags)
            .ToListAsync();

        recipes = recipes.Where(r => r.Ingredients.Any(i => requestIngredients.Contains(i))).ToList();

        if (request.SortBy == "maximize-used")
        {
            recipes = recipes.OrderByDescending(r => r.Ingredients.Count(i => requestIngredients.Contains(i))).ToList();
        }

        if (request.SortBy == "minimize-missing")
        {
            recipes = recipes.OrderBy(r => r.Ingredients.Count(i => !requestIngredients.Contains(i))).ToList();
        }

        if (request.SortBy == "only-these")
        {
            recipes = recipes.Where(r => !r.Ingredients.Any(i => !requestIngredients.Contains(i))).ToList();
        }

        return recipes;
    }


    public async Task<Recipe?> GetRecipeById(int id)
    {
        var recipe = await _dataContext.Recipes.Include(r => r.Ingredients).ThenInclude(ri => ri.Image)
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .Include(r => r.CreatedBy)
            .Include(r => r.ThumbnailImage)
            .Include(r => r.RecipeTags).FirstOrDefaultAsync(r => r.Id == id);
        return recipe;
    }
}