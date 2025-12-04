using FastEndpoints;
using FluentValidation;
using TemplateProject.Api.Endpoints.Orders.DTOs;

namespace TemplateProject.Api.Endpoints.Orders.Validators;

public class UpdateOrderRequestValidator : Validator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
