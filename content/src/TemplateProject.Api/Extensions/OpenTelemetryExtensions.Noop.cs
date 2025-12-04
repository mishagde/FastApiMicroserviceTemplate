using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.Api.Extensions;

public static class OpenTelemetryExtensions
{
    // No-op implementation used when OpenTelemetry is disabled in the template options
    public static IServiceCollection AddOpenTelemetryTelemetry(this IServiceCollection services, IConfiguration config)
    {
        // Intentionally do nothing when OTEL is disabled
        return services;
    }
}
