using FastEndpoints;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;
using IMapper = MapsterMapper.IMapper;

namespace TemplateProject.Api.Endpoints.Orders;

public class GetOrdersByCustomerEndpoint : EndpointWithoutRequest<List<OrderDto>>
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public GetOrdersByCustomerEndpoint(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override void Configure()
    {
        // Можно использовать query или route:
        // GET /orders/customer/john
        // Или GET /orders/by-customer?name=john
        Get("/orders/customer/{name}");
        AllowAnonymous(); // или роли, если нужно

        Summary(s =>
        {
            s.Summary = "Получить список заказов по имени клиента";
            s.Description = "Возвращает список заказов, принадлежащих указанному клиенту.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerName = Route<string>("name");

        if (string.IsNullOrWhiteSpace(customerName))
        {
            ThrowError("Customer name is required.");
            return;
        }

        var orders = await _service.GetOrdersByCustomerAsync(customerName, ct);

        var result = _mapper.Map<List<OrderDto>>(orders);

        await SendAsync(result, cancellation: ct);
    }
}
