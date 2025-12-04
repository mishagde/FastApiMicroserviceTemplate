using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TemplateProject.Api.Endpoints.Auth;

public class AuthInfoEndpoint : EndpointWithoutRequest<AuthInfoResponse>
{
    public override void Configure()
    {
        Get("/auth/me");
        Roles("user", "admin");     // или AllowAnonymous() — но лучше требовать JWT
        Summary(s =>
        {
            s.Summary = "Возвращает информацию о текущем пользователе на основе JWT токена";
            s.Description = "Микросервис не выдаёт токены, а только читает их. Этот endpoint показывает данные пользователя, взятые из JWT.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var response = new AuthInfoResponse
        {
            UserId = user.FindFirstValue(ClaimTypes.NameIdentifier),
            Email  = user.FindFirstValue(ClaimTypes.Email),
            Roles  = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
        };

        await SendAsync(response, cancellation: ct);
    }
}
