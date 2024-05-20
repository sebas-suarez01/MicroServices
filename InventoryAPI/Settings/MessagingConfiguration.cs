using InventoryAPI.Consumers;
using MassTransit;

namespace InventoryAPI.Settings;

public static class MessagingConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            busConfig.AddConsumer<BookAvailabilityConsumer>();
            busConfig.AddConsumer<BookBoughtEventConsumer>();
            
            busConfig.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:HOST"]!), h =>
                {
                    h.Username(configuration["MessageBroker:USERNAME"]!);
                    h.Password(configuration["MessageBroker:PASSWORD"]!);
                });
    
                configurator.ConfigureEndpoints(context);
            });
    
        });
        return services;
    }
}