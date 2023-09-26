using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly DataContext _dataContext;

    public IngredientRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Ingredient> Create(Ingredient ingredient)
    {
        await _dataContext.AddAsync(ingredient);
        await _dataContext.SaveChangesAsync();
        return ingredient;
    }
    
    public async Task<Ingredient?> GetByIdAsync(int id)
    {
       var ingredient = await _dataContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
        return ingredient;
    }

    public async Task<List<Ingredient>?> GetIngredientsByNameAsync(string name)
    {
        var ingredients =  await _dataContext.Ingredients
            .Where(i => i.Name.ToLower().StartsWith(name.ToLower())).ToListAsync();
       return ingredients;
    }
}