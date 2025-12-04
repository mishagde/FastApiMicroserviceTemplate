using FastEndpoints;
using TemplateProject.Application.Interfaces;

namespace TemplateProject.Api.Endpoints.Orders;

public class DeleteOrderEndpoint : EndpointWithoutRequest
{
    private readonly IOrderService _service;

    public DeleteOrderEndpoint(IOrderService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Delete("/orders/{id:int}");
        AllowAnonymous(); // заменить на RequireAuthorization() при необходимости

        Summary(s =>
        {
            s.Summary = "Удалить заказ";
            s.Description = "Удаляет заказ по ID, если он существует.";
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

        await _service.DeleteOrderAsync(order, ct);

        await SendNoContentAsync(ct);
    }
}
