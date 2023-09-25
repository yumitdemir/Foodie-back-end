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
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientsController(IImageService imageService, IImageRepository imageRepository,
        IIngredientRepository ingredientRepository)
    {
        _imageService = imageService;
        _imageRepository = imageRepository;
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] IngredientCreateRequestDto request)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Upload Image
        try
        {
            var file = new FileUploadDto
            {
                File = request.File,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            await _imageService.UploadImageAsync(file);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("file", e.Message);
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

        PriceUnitType? priceUnit = null;
        if (!string.IsNullOrEmpty(request.PriceUnit) &&
            Enum.TryParse<PriceUnitType>(request.PriceUnit, true, out PriceUnitType parsedPriceUnit))
        {
            var ingredinet = new Ingredient
            {
                Name = request.Name,
                Desccription = request.Desccription,
                ImageId = image.Id,
                Nutrition = request.Nutrition,
                PriceUnit = parsedPriceUnit,
                UnitPrice = request.UnitPrice,
            };

            await _ingredientRepository.Create(ingredinet);
            return Ok();
        }

        ModelState.AddModelError("priceUnit", "PriceUnit is not in correct structure");
        return BadRequest(ModelState);
    }
}