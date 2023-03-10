using GenericControllerDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GenericControllerDemo.Data;

public class GenericDbContext : DbContext
{
    public static IModel StaticModel { get; } = BuildStaticModel();

    public DbSet<First> FirstTestModels { get; set; }
    public DbSet<Second> SecondTestModels { get; set; }
    public DbSet<Third> ThirdTestModels { get; set; }
    public DbSet<Unrelated> Unrelateds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("ApplicationDb");
        }
    }

    private static IModel BuildStaticModel()
    {
        using var dbContext = new GenericDbContext();
        return dbContext.Model;
    }
}