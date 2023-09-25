using WebApplication1.Dto;

namespace WebApplication1.Interfaces;

public interface IImageService
{
    public void ValidateFileUpload(IngredientCreateRequestDto request);

}