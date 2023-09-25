using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Dto;

public class RecipeCreateRequestDto
{

    
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string FileName { get; set; }
    public string? FileDescription { get; set; }

    
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int UserId { get; set; }

    public List<int> IngredientIds { get; set; }

    public List<RecipeStep> RecipeSteps { get; set; }

    public int? ServingSize { get; set; }
    public string? PreparationTime { get; set; }

}