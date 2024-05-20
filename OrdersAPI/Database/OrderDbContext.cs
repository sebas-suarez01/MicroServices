using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;

namespace OrdersAPI.Database;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderModel>().HasKey(b => b.Id);
        modelBuilder.Entity<OrderModel>().Property(b => b.Id)
            .ValueGeneratedNever();
        modelBuilder.Entity<OrderModel>().HasMany(o => o.OrderBooks)
            .WithOne();
        
        modelBuilder.Entity<OrderBooks>().HasKey(ob => new { ob.OrderId, ob.BookId });
        modelBuilder.Entity<OrderBooks>().HasOne(om => om.Book)
            .WithMany();
        modelBuilder.Entity<OrderBooks>().HasOne(om => om.Order)
            .WithMany(o=> o.OrderBooks);
        
        modelBuilder.Entity<BookModel>().HasKey(b => b.Id);
        modelBuilder.Entity<BookModel>().Property(b => b.Id)
            .ValueGeneratedNever();
        
    }
}