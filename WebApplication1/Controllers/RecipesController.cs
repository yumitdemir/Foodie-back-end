﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IImageRepository _imageRepository;
    private readonly IImageService _imageService;
    private readonly IUserRepository _userRepository;

    public RecipesController(IImageRepository imageRepository, IImageService imageService,
        IUserRepository userRepository)
    {
        _imageRepository = imageRepository;
        _imageService = imageService;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] RecipeCreateRequestDto request)
    {
        // Model Checks
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _userRepository.GetByIdAsync(request.UserId) == null)
        {
            NotFound("User not found");
        }

        bool isPreparationRightFormat = TimeSpan.TryParse(request.PreparationTime, out TimeSpan preparationTime);
        if (!isPreparationRightFormat)
        {
            ModelState.AddModelError("PreparationTime", "PreparationTime is not in the correct format");
            return BadRequest(ModelState);
        }

        // Upload Image
        Image thumbnailImage = new Image();
        try
        {
            var file = new FileUploadDto
            {
                File = request.File,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            thumbnailImage = await _imageService.UploadImageAsync(file);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("file", e.Message);
            BadRequest(ModelState);
        }

        //Recipe Creation
        var recipeModel = new Recipe
        {
            Description = request.Description,
            Title = request.Title,
            CreatedAt = DateTime.Now,
            UserId = request.UserId,
            PreparationTime = preparationTime,
            ServingSize = request.ServingSize,
            ImageId = thumbnailImage.Id,
            
        };


        return BadRequest(ModelState);
    }
}