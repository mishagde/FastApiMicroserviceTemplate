namespace TemplateProject.Infrastructure.Messaging.Contracts;

public record OrderCreatedEvent(int OrderId, string Customer, decimal Amount);
