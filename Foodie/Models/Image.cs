using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Image
{
    [Key] 
    public int Id { get; set; }
    
    [NotMapped]
    public IFormFile File { get; set; }

    public string FileName { get; set; }
    
    public string? Description { get; set; }
    
    public string FileExtension { get; set; }

    public long FileSizeInBytes { get; set; }

    public string FilePath { get; set; }
}