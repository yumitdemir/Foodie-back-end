using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeTagsController : ControllerBase
{
    private readonly ITagRepository _tagRepository;

    public RecipeTagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    [HttpPost]
    [Route("CreateTag")]
    public async Task<IActionResult> Create(RecipeTagCreateRequestDto request)
    {
        var tag = new RecipeTag
        {
            Description = request.Description,
            Name = request.Name
        };
        tag = await _tagRepository.Create(tag);
        return Ok(tag);
    }
}