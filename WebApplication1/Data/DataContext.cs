using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Image> Images { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>().OwnsOne(i => i.Nutrition);


        modelBuilder.Entity<RecipeStep>()
            .HasOne(rs => rs.Image)
            .WithMany()
            .HasForeignKey(rs => rs.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

       

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.UserCommented)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);
    }
}