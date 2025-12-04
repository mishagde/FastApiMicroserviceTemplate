using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyService.Domain.Common;

namespace TemplateProject.Application.Interfaces;

public interface IRepository<T> where T : EntityBase, IAggregateRoot
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<T> AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
}
