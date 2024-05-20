using CatalogAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Settings;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<CatalogDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DbConnectionDocker")));
        return services;
    }
}