using TemplateProject.Domain.Entities;

namespace TemplateProject.Application.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string customer, decimal amount, CancellationToken ct = default);
    Task<Order?> GetOrderAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Order>> GetOrdersByCustomerAsync(string customer, CancellationToken ct = default);
    Task UpdateOrderAsync(Order order, CancellationToken ct = default);
    Task DeleteOrderAsync(Order order, CancellationToken ct = default);
}
