using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IRecipeRepository
{
    public Task<Recipe> Create(Recipe recipe);
    public  Task<List<Recipe?>> GetRecipesByFilter(SearchRecipeRequestDto request);
    public Task<Recipe?> GetRecipeById(int id);



}