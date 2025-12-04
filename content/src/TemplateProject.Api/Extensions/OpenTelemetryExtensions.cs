#if (use-opentelemetry)
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TemplateProject.Api.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetryTelemetry(this IServiceCollection services, IConfiguration config)
    {
        var serviceName = config["Service:Name"] ?? "TemplateProject";

        services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService(serviceName))
            .WithTracing(trace =>
            {
                trace.AddAspNetCoreInstrumentation()
                     .AddHttpClientInstrumentation()
                     .AddEntityFrameworkCoreInstrumentation()
                     .AddOtlpExporter(otlp =>
                     {
                         otlp.Endpoint = new Uri(config["OpenTelemetry:OtlpEndpoint"]!);
                     });
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddRuntimeInstrumentation()
                       .AddOtlpExporter(otlp =>
                       {
                           otlp.Endpoint = new Uri(config["OpenTelemetry:OtlpEndpoint"]!);
                       });
            });

        return services;
    }
}
#endif
