using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Recipe
{
    [Key] public int Id { get; set; }

    public int ImageId { get; set; }
    [ForeignKey("ImageId")] public Image ThumbnailImage { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }


    public int UserId { get; set; }
    [ForeignKey("UserId")] public User CreatedBy { get; set; }

    public List<Ingredient> Ingredients { get; set; }

    public List<RecipeStep> Steps { get; set; }

    public int? ServingSize { get; set; }
    public TimeSpan? PreparationTime { get; set; }
}