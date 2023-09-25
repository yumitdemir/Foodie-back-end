using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DataContext _dataContext;

    public RecipeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Recipe> Create(Recipe recipe)
    {
        await _dataContext.Recipes.AddAsync(recipe);
        await _dataContext.SaveChangesAsync();
        return recipe;
    }

 
}