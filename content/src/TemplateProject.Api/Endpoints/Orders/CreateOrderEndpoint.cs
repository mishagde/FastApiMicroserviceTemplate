using FastEndpoints;
using Mapster;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;

namespace TemplateProject.Api.Endpoints.Orders;

public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderService _service;

    public CreateOrderEndpoint(IOrderService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("/orders");
        AllowAnonymous(); // можно заменить на JWT
        Summary(s =>
        {
            s.Summary = "Создать новый заказ";
            s.Description = "Создаёт заказ и публикует событие OrderCreatedEvent в RabbitMQ.";
        });
    }

    public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
    {
        var order = await _service.CreateOrderAsync(req.Customer, req.Amount, ct);

        var response = order.Adapt<CreateOrderResponse>();

        await SendAsync(response, cancellation: ct);
    }
}
