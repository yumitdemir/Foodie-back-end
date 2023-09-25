using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IImageRepository _imageRepository;

    public IngredientsController(IImageService imageService, IImageRepository imageRepository)
    {
        _imageService = imageService;
        _imageRepository = imageRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] IngredientCreateRequestDto request)
    {
        try
        {
            _imageService.ValidateFileUpload(request);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("file", e.Message);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var imageModel = new Image
        {
            File = request.File,
            FileName = request.FileName,
            FileExtension = Path.GetExtension(request.File.FileName),
            FileSizeInBytes = request.FileName.Length,
            Description = request.FileDescription
        };
        var image = await _imageRepository.Upload(imageModel);


        return Ok();
    }
}