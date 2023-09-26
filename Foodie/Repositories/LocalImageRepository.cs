using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _dataContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
        DataContext dataContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dataContext = dataContext;
    }

    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",$"{image.FileName}{image.FileExtension}");

        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var urlFilePath =
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

        image.FilePath = urlFilePath;

        await _dataContext.Images.AddAsync(image);
        await _dataContext.SaveChangesAsync();

        return image;
    }
}