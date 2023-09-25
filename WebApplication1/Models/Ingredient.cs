using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Ingredient
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
}