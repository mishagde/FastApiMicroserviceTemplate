using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddRabbitMqBus(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            // Automatically load consumers from Infrastructure
            x.AddConsumers(typeof(Program).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitHost = config.GetConnectionString("RabbitMq");

                cfg.Host(rabbitHost);

                // Automate queue creation + configure endpoints
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
