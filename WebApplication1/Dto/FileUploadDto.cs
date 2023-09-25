namespace WebApplication1.Dto;

public class FileUploadDto
{
    public IFormFile File { get; set; }
    public string FileName { get; set; }
    public string? FileDescription { get; set; }
}