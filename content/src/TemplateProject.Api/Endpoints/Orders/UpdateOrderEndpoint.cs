using FastEndpoints;
using MapsterMapper;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;
using TemplateProject.Domain.Entities;

namespace TemplateProject.Api.Endpoints.Orders;

public class UpdateOrderEndpoint : Endpoint<UpdateOrderRequest, OrderDto>
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public UpdateOrderEndpoint(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Put("/orders/{id:int}");
        AllowAnonymous(); // или RequireAuthorization()
        Summary(s =>
        {
            s.Summary = "Обновить заказ";
            s.Description = "Изменяет данные заказа по ID.";
        });
    }

    public override async Task HandleAsync(UpdateOrderRequest req, CancellationToken ct)
    {
        var id = Route<int>("id");

        var order = await _service.GetOrderAsync(id, ct);

        if (order is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Доменная логика обновления
        order.UpdateAmount(req.Amount);

        // Сохраняем изменения
        // Здесь мы обращаемся к репозиторию через сервис
        // Но можно улучшить IOrderService, добавив метод UpdateOrderAsync.
        await _service.UpdateOrderAsync(order, ct);

        var dto = _mapper.Map<OrderDto>(order);

        await SendAsync(dto, cancellation: ct);
    }
}
