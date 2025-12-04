using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyService.Domain.Entities;

namespace TemplateProject.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    // Here you can add domain-specific queries
    Task<IReadOnlyList<Order>> GetByCustomerAsync(string customer, CancellationToken ct = default);
}
