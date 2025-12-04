using System;
using TemplateProject.Domain.Common;

namespace TemplateProject.Domain.Entities;

public class Order : EntityBase, IAggregateRoot
{
    public string Customer { get; private set; } = default!;
    public decimal Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Optional: Domain events can be added here later
    // private readonly List<IDomainEvent> _events = new();
    // public IReadOnlyCollection<IDomainEvent> Events => _events;

    // EF Core constructor
    private Order() { }

    public Order(string customer, decimal amount)
    {
        Customer = customer;
        Amount = amount;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateAmount(decimal newAmount)
    {
        Amount = newAmount;
    }
}
