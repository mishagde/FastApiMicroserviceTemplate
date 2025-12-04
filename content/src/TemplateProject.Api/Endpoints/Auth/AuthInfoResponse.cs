namespace TemplateProject.Api.Endpoints.Auth;

public class AuthInfoResponse
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new();
}
