using AcademiaNet.Shared.Entites;
using Microsoft.EntityFrameworkCore; // ORM nos permite trrabajar las bd relacionales

namespace AcademiaNet.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Institution> Institutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Institution>().HasIndex(x => x.Name).IsUnique();
    }
}