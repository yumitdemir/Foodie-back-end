using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IRecipeRepository
{
    public Task<Recipe> Create(Recipe recipe);

}