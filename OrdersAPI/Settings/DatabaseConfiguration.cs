using Microsoft.EntityFrameworkCore;
using OrdersAPI.Database;

namespace OrdersAPI.Settings;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<OrderDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DbConnectionDocker")));
        return services;
    }
}