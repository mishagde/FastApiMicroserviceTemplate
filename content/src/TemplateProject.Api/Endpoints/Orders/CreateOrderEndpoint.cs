using FastEndpoints;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Application.Interfaces;
using IMapper = MapsterMapper.IMapper;

namespace TemplateProject.Api.Endpoints.Orders;

public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public CreateOrderEndpoint(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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

        var response = _mapper.Map<CreateOrderResponse>(order);

        await SendAsync(response, cancellation: ct);
    }
}
