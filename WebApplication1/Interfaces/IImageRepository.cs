using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
}