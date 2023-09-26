using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DataContext _dataContext;

    public TagRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<RecipeTag> Create(RecipeTag recipeTag)
    {
        await _dataContext.RecipeTags.AddAsync(recipeTag);
        await _dataContext.SaveChangesAsync();

        return recipeTag;
    }

    public async Task<RecipeTag?> GetByIdAsync(int id)
    {
        var tag =  await _dataContext.RecipeTags.FirstOrDefaultAsync(rt => rt.Id == id);
        return tag;
    }
}