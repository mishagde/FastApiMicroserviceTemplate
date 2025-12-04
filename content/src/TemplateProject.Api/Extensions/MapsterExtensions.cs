using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.Api.Extensions;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(Program).Assembly);

        services.AddSingleton(config);
        // No DI mapper; use Mapster static Adapt<T> API instead.

        return services;
    }
}
