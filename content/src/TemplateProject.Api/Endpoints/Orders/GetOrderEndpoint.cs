using FastEndpoints;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;
using IMapper = MapsterMapper.IMapper;

namespace TemplateProject.Api.Endpoints.Orders;

public class GetOrderEndpoint : EndpointWithoutRequest<OrderDto>
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public GetOrderEndpoint(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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

        var dto = _mapper.Map<OrderDto>(order);

        await SendAsync(dto, cancellation: ct);
    }
}
