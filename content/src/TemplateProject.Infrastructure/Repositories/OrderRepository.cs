using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Application.Interfaces;
using TemplateProject.Domain.Entities;
using TemplateProject.Infrastructure.Data;

namespace TemplateProject.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext db)
        : base(db)
    {
    }

    public async Task<IReadOnlyList<Order>> GetByCustomerAsync(string customer, CancellationToken ct = default)
    {
        return await _db.Orders
            .Where(o => o.Customer == customer)
            .ToListAsync(ct);
    }
}
