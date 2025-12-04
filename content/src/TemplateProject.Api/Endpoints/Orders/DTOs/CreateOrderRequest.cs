namespace TemplateProject.Api.Endpoints.Orders.DTOs;

public class CreateOrderRequest
{
    public string Customer { get; set; } = default!;
    public decimal Amount { get; set; }
}
