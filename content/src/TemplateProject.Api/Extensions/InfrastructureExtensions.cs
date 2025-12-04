using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Application.Interfaces;
using TemplateProject.Application.Services;
using TemplateProject.Infrastructure.Repositories;

namespace TemplateProject.Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddApplicationAndInfrastructure(this IServiceCollection services)
    {
        // -----------------------------
        // Application Services
        // -----------------------------
        services.AddScoped<IOrderService, OrderService>();

        // -----------------------------
        // Repositories
        // -----------------------------
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
