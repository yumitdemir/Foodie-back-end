using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto;

public class IngredientCreateRequestDto
{
    //img
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string FileName { get; set; }
    public string? FileDescription { get; set; }
    
    // Ingredient 
    public decimal? UnitPrice { get; set; }
    public string? PriceUnit { get; set; }
    public NutritionalInformation? Nutrition { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Desccription { get; set; }
    
    
    
}