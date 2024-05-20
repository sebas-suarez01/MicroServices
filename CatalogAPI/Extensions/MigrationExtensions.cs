using CatalogAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using CatalogDbContext dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        
        dbContext.Database.Migrate();
    }
}