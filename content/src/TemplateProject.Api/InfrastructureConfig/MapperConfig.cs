using Mapster;
using TemplateProject.Api.Endpoints.Orders.DTOs;
using TemplateProject.Domain.Entities;

namespace TemplateProject.Api.InfrastructureConfig;

public class MapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // --------------------------
        // Domain → DTO
        // --------------------------
        config.ForType<Order, OrderDto>();

        config.ForType<Order, CreateOrderResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);

        // --------------------------
        // DTO → Domain (optional)
        // --------------------------
        // Обычно Domain создаётся через ctor, но можно мапить в Request
        config.ForType<CreateOrderRequest, Order>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CreatedAt);
    }
}
