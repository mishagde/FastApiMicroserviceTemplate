using Serilog;
using System.Net;
using System.Text.Json;

namespace TemplateProject.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred while processing request");

            await WriteErrorResponseAsync(context, ex);
        }
    }

    private static Task WriteErrorResponseAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Message = "Internal Server Error",
            Detail = ex.Message,
            TraceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public string Message { get; set; } = default!;
        public string Detail { get; set; } = default!;
        public string TraceId { get; set; } = default!;
    }
}
