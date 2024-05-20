using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Database;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookModel>().HasKey(b => b.Id);
        modelBuilder.Entity<BookModel>().Property(b => b.Id)
            .ValueGeneratedNever();
        
    }
}