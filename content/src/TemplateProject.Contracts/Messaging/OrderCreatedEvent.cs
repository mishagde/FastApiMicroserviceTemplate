namespace TemplateProject.Contracts.Messaging;

public record OrderCreatedEvent(int OrderId, string Customer, decimal Amount);
