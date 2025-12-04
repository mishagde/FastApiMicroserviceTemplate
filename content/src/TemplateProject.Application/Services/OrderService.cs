using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MyService.Application.Interfaces;
using MyService.Domain.Entities;
using MyService.Infrastructure.Messaging.Contracts;

namespace TemplateProject.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IPublishEndpoint _publisher;

    public OrderService(IOrderRepository repo, IPublishEndpoint publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    public async Task<Order> CreateOrderAsync(string customer, decimal amount, CancellationToken ct = default)
    {
        var order = new Order(customer, amount);

        // Сохраняем заказ в БД
        await _repo.AddAsync(order, ct);

        // Публикуем доменное событие в RabbitMQ через MassTransit
        await _publisher.Publish(new OrderCreatedEvent(order.Id, customer, amount), ct);

        return order;
    }

    public Task<Order?> GetOrderAsync(int id, CancellationToken ct = default)
    {
        return _repo.GetByIdAsync(id, ct);
    }

    public Task<IReadOnlyList<Order>> GetOrdersByCustomerAsync(string customer, CancellationToken ct = default)
    {
        return _repo.GetByCustomerAsync(customer, ct);
    }

    public async Task DeleteOrderAsync(Order order, CancellationToken ct = default)
    {
        await _repo.DeleteAsync(order, ct);
    }
}
