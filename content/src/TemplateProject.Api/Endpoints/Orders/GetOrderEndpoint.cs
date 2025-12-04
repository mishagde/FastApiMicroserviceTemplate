using FastEndpoints;
using Mapster;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;

namespace TemplateProject.Api.Endpoints.Orders;

public class GetOrderEndpoint : EndpointWithoutRequest<OrderDto>
{
    private readonly IOrderService _service;

    public GetOrderEndpoint(IOrderService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/orders/{id:int}");
        AllowAnonymous(); // можно заменить на JWT
        Summary(s =>
        {
            s.Summary = "Получить заказ по ID";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var order = await _service.GetOrderAsync(id, ct);

        if (order is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var dto = order.Adapt<OrderDto>();

        await SendAsync(dto, cancellation: ct);
    }
}
