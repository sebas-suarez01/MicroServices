using InventoryAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Settings;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<InventoryDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DbConnectionDocker")));
        return services;
    }
}