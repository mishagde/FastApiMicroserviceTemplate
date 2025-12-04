using FastEndpoints;
using FastEndpoints.Swagger;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TemplateProject.Api.Extensions;
using TemplateProject.Api.InfrastructureConfig;
using TemplateProject.Api.Middleware;
using TemplateProject.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// ------------------------------------------------------
// Logging (Serilog)
// ------------------------------------------------------
builder.Host.UseSerilogWithOpenSearch();

// ------------------------------------------------------
// Services Registration
// ------------------------------------------------------
builder.Services.AddApplicationAndInfrastructure();

// EF Core
builder.Services.AddPostgres(config);

// Redis
builder.Services.AddRedis(config);

// MassTransit + RabbitMQ
builder.Services.AddRabbitMqBus(config);

// Authentication (JWT from external auth service)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

// Authorization
builder.Services.AddAuthorization();

// Mapster + IMapper
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(typeof(MapperConfig).Assembly);

builder.Services.AddSingleton(typeAdapterConfig);

// FastEndpoints
builder.Services.AddFastEndpoints();

// Swagger
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "TemplateProject API";
        s.Version = "v1";
    };
});

// HealthChecks
builder.Services.AddHealthChecks()
    .AddNpgSql(config.GetConnectionString("Postgres")!)
    .AddRedis(config.GetConnectionString("Redis")!);

// OpenTelemetry
builder.Services.AddOpenTelemetryTelemetry(config);

var app = builder.Build();

// ------------------------------------------------------
// Middleware Pipeline
// ------------------------------------------------------
app.UseGlobalExceptionHandling();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

app.UseOpenApi();     // FastEndpoints Swagger
app.UseSwaggerUi();   // Swagger UI

app.MapHealthChecks("/health");

app.ApplyMigrations();

app.Run();
