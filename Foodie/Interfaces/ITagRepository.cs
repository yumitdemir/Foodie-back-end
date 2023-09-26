using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface ITagRepository
{
    public Task<RecipeTag> Create(RecipeTag recipeTag);
    public Task<RecipeTag?> GetByIdAsync(int id);
}