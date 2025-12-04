using FastEndpoints;
using FluentValidation;
using TemplateProject.Api.Endpoints.Orders.DTOs;

namespace TemplateProject.Api.Endpoints.Orders.Validators;

public class CreateOrderRequestValidator : Validator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Customer)
            .NotEmpty().WithMessage("Customer is required.")
            .MaximumLength(200).WithMessage("Customer name is too long.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
