using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IImageService
{
    public void ValidateFileUpload(FileUploadDto request);
    public Task<Image> UploadImageAsync(FileUploadDto request);


}