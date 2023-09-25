using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;
[ModelBinder(BinderType = typeof(RecipeStepRequestModelBinder))]
public  class RecipeStepRequest
{
    public int StepNo { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    
}