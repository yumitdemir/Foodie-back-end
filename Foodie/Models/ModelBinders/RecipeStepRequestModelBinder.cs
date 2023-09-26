using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

public class RecipeStepRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var values = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (values.Length == 0)
            return Task.CompletedTask;

        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        var deserialized = JsonSerializer.Deserialize<WebApplication1.Models.RecipeStepRequest>(values.FirstValue, options);

        bindingContext.Result = ModelBindingResult.Success(deserialized);
        return Task.CompletedTask;
    }
}