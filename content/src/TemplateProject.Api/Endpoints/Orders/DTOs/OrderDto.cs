namespace TemplateProject.Api.Endpoints.Orders.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string Customer { get; set; } = default!;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
