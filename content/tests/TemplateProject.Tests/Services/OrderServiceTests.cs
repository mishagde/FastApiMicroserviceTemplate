using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using MassTransit;
using MyService.Application.Interfaces;
using MyService.Application.Services;
using MyService.Domain.Entities;
using MyService.Infrastructure.Messaging.Contracts;
using Xunit;

namespace TemplateProject.Tests.Services;

public class OrderServiceTests
{
    private readonly IOrderRepository _repo = Substitute.For<IOrderRepository>();
    private readonly IPublishEndpoint _publisher = Substitute.For<IPublishEndpoint>();
    private readonly IOrderService _service;

    public OrderServiceTests()
    {
        _service = new OrderService(_repo, _publisher);
    }

    [Fact]
    public async Task CreateOrderAsync_Should_Create_Order_And_Publish_Event()
    {
        // arrange
        var customer = "John";
        var amount = 100m;

        Order? savedOrder = null;

        _repo.AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(ci =>
            {
                savedOrder = ci.Arg<Order>();
                savedOrder.Id = 1; // EF normally assigns Id
                return savedOrder!;
            });

        // act
        var result = await _service.CreateOrderAsync(customer, amount);

        // assert
        result.Should().NotBeNull();
        result.Customer.Should().Be(customer);
        result.Amount.Should().Be(amount);
        result.Id.Should().Be(1);

        await _publisher.Received(1).Publish(
            Arg.Is<OrderCreatedEvent>(e =>
                e.OrderId == 1 &&
                e.Customer == customer &&
                e.Amount == amount),
            Arg.Any<CancellationToken>()
        );

        await _repo.Received(1).AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetOrderAsync_Should_Return_Order_When_Found()
    {
        // arrange
        var order = new Order("John", 50) { Id = 10 };

        _repo.GetByIdAsync(10, Arg.Any<CancellationToken>())
            .Returns(order);

        // act
        var result = await _service.GetOrderAsync(10);

        // assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(10);
        result.Customer.Should().Be("John");
    }

    [Fact]
    public async Task GetOrderAsync_Should_Return_Null_When_Not_Found()
    {
        _repo.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        // act
        var result = await _service.GetOrderAsync(1);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetOrdersByCustomerAsync_Should_Return_List()
    {
        // arrange
        var list = new List<Order>
        {
            new("John", 10) { Id = 1 },
            new("John", 20) { Id = 2 }
        };

        _repo.GetByCustomerAsync("John", Arg.Any<CancellationToken>())
            .Returns(list);

        // act
        var result = await _service.GetOrdersByCustomerAsync("John");

        // assert
        result.Should().HaveCount(2);
        result[0].Customer.Should().Be("John");
    }

    [Fact]
    public async Task UpdateOrderAsync_Should_Save_Changes()
    {
        // arrange
        var order = new Order("John", 100) { Id = 5 };

        // act
        await _service.UpdateOrderAsync(order);

        // assert
        await _repo.Received(1).UpdateAsync(order, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteOrderAsync_Should_Remove_Order()
    {
        // arrange
        var order = new Order("John", 100) { Id = 5 };

        // act
        await _service.DeleteOrderAsync(order);

        // assert
        await _repo.Received(1).DeleteAsync(order, Arg.Any<CancellationToken>());
    }
}
