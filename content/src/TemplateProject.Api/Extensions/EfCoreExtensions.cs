using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Infrastructure.Data;

namespace TemplateProject.Api.Extensions;

public static class EfCoreExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("Postgres");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connection);
        });

        return services;
    }
}
