using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    
    public void ValidateFileUpload(FileUploadDto request)
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

    public async Task<Image> UploadImageAsync(FileUploadDto request)
    {
        // Validate the file
        var file = new FileUploadDto
        {
            File = request.File,
            FileName = request.FileName,
            FileDescription = request.FileDescription
        };
        ValidateFileUpload(file);

        // Create the image model
        var imageModel = new Image
        {
            File = request.File,
            FileName = request.FileName,
            FileExtension = Path.GetExtension(request.File.FileName),
            FileSizeInBytes = request.FileName.Length,
            Description = request.FileDescription
        };

        // Upload the image and return the result
        return await _imageRepository.Upload(imageModel);
    }
}