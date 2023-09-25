using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class RecipeStep
{
    [Key] public int Id { get; set; }
    public int StepNo { get; set; }
    public string Description { get; set; }
    public int ImageId { get; set; }
    [ForeignKey("ImageId")] 
    public Image Image { get; set; }

   
}