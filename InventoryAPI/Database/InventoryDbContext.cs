using InventoryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Database;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookModel>().HasKey(b => b.Id);
        modelBuilder.Entity<BookModel>().Property(b => b.Id)
            .ValueGeneratedNever();
    }
}