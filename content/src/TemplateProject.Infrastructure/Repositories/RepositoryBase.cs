using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Application.Interfaces;
using TemplateProject.Domain.Common;
using TemplateProject.Infrastructure.Data;

namespace TemplateProject.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepository<T>
    where T : EntityBase, IAggregateRoot
{
    protected readonly AppDbContext _db;

    protected RepositoryBase(AppDbContext db)
    {
        _db = db;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.Set<T>().FindAsync(new object[] { id }, ct);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Set<T>().ToListAsync(ct);
    }

    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        await _db.Set<T>().AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
