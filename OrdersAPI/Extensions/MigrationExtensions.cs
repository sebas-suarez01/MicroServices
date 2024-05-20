using Microsoft.EntityFrameworkCore;
using OrdersAPI.Database;

namespace OrdersAPI.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using OrderDbContext dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        
        dbContext.Database.Migrate();
    }
}