using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IIngredientRepository
{
    public Task<Ingredient> Create(Ingredient ingredient);
    public  Task<Ingredient?> GetByIdAsync(int id);
    public  Task<List<Ingredient>?> GetIngredientsByNameAsync(string name);
}