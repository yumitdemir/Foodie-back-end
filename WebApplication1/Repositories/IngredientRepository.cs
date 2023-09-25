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
}