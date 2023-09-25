using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
}