using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Comment
{
    [Key] public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User UserCommented { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }

    public int RecipeId { get; set; }
    [ForeignKey("RecipeId")]
    public Recipe RecipeCommented { get; set; }
}