using WebApplication1.Dto;
using WebApplication1.Interfaces;

namespace WebApplication1.Services;

public class ImageService : IImageService
{
    public void ValidateFileUpload(IngredientCreateRequestDto request)
    {
        var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

        if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
        {
            throw new Exception("Unsupported file extension");
        }

        if (request.File.Length > 10485760)
        {
            throw new Exception("File size is more than 10MB");
        }
    }
}