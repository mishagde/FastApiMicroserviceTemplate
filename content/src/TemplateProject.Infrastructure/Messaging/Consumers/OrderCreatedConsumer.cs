using MassTransit;
using Microsoft.Extensions.Logging;
using TemplateProject.Infrastructure.Messaging.Contracts;

namespace TemplateProject.Infrastructure.Messaging.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var msg = context.Message;

        _logger.LogInformation(
            "OrderCreatedConsumer received event: OrderId={OrderId}, Customer={Customer}, Amount={Amount}",
            msg.OrderId,
            msg.Customer,
            msg.Amount);

        // здесь можно делать что угодно:
        // - отправить email/SMS
        // - создать запись в Outbox/EventStore
        // - вызвать сторонний API
        // - сохранить в отдельную таблицу статистики

        return Task.CompletedTask;
    }
}
