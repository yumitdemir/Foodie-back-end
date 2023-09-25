using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class IngredientCreateRequestDto
{
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string FileName { get; set; }

    public string? FileDescription { get; set; }
}