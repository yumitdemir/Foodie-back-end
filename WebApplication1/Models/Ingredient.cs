using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Ingredient
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public string? Desccription { get; set; }
    public decimal? UnitPrice { get; set; }
    public PriceUnitType? PriceUnit { get; set; }
    public NutritionalInformation? Nutrition { get; set; }

    public int? ImageId { get; set; }
    [ForeignKey("ImageId")] 
    public Image Image { get; set; }
}

public enum PriceUnitType
{
    PerGram,
    PerKilogram,
    PerOunce,
    PerPound,
    PerLiter,
    PerMilliliter
}