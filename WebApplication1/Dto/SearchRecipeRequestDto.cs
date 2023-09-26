namespace WebApplication1.Dto;

public class SearchRecipeRequestDto
{
    public List<int> IngredientIds { get; set; }
    public bool IgnoreTypicalPantry { get; set; }
    public string SortBy { get; set; } = "maximize-used";
}