using FastEndpoints;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TemplateProject.Api.Endpoints.Health;

public class HealthCheckEndpoint : EndpointWithoutRequest
{
    private readonly HealthCheckService _healthChecks;

    public HealthCheckEndpoint(HealthCheckService healthChecks)
    {
        _healthChecks = healthChecks;
    }

    public override void Configure()
    {
        Get("/health");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Проверка состояния сервиса и подключений";
            s.Description = "Проверяет доступность PostgreSQL, Redis, RabbitMQ и других зависимостей.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var report = await _healthChecks.CheckHealthAsync(ct);

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            }),
            totalDuration = report.TotalDuration.ToString()
        };

        if (report.Status == HealthStatus.Healthy)
        {
            await SendOkAsync(response, ct);
        }
        else
        {
            await SendAsync(response, 503, ct); // service unavailable
        }
    }
}
