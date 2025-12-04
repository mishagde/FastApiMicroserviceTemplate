using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.OpenSearch;

namespace TemplateProject.Api.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder UseSerilogWithOpenSearch(this IHostBuilder host)
    {
        host.UseSerilog((ctx, cfg) =>
        {
            cfg.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console();

            var osUrl = ctx.Configuration["Serilog:OpenSearch:Url"];
            if (!string.IsNullOrWhiteSpace(osUrl))
            {
                cfg.WriteTo.OpenSearch(new OpenSearchSinkOptions(new Uri(osUrl))
                {
                    IndexFormat = "logs-templateproject-{0:yyyy.MM}",
                    AutoRegisterTemplate = true,
                    NumberOfShards = 1,
                    NumberOfReplicas = 1
                });
            }
        });

        return host;
    }
}
