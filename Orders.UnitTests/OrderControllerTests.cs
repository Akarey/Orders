using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Orders.API;
using Orders.API.Controllers;
using Orders.API.Services;

namespace Orders.UnitTests;

public class OrderControllerTests
{
    private OrderController OrderController;
    private Mock<IDataService> mockService;


    public OrderControllerTests()
    {
        mockService = new Mock<IDataService>();
    
        OrderController = new OrderController(mockService.Object);
    }


    [Fact]
    public void ListOrders_OnSuccess_ReturnsStatusCode200()
    {
        OkObjectResult result = (OkObjectResult) OrderController.ListOrders();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void AddOrder_OnSuccess_ReturnsStatusCode200()
    {
        Order order = SampleOrders.genericOrder;

        OkObjectResult result = (OkObjectResult)OrderController.AddOrder(order);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void AddOrder_OnInvalidOrder_ReturnsStatusCode400()
    {
        Order order = SampleOrders.deliveryDateInThePast;

        BadRequestResult result = (BadRequestResult)OrderController.AddOrder(order);

        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public void ListOrders_Calls_GetOrdersJsonString()
    {
        OrderController.ListOrders();

        mockService.Verify(mock => mock.GetOrdersJsonString(), Times.Once);
    }

    [Fact]
    public void ValidOrders_ArePassedInto_AddOrder()
    {
        SampleOrders.validOrders.ForEach(order => {
            OrderController.AddOrder(order);

            mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x =>
                x.kitsNumber == order.kitsNumber &&
                x.customerId == order.customerId &&
                x.deliveryDate == order.deliveryDate
            )), Times.Once);
        });
    }

    [Fact]
    public void InvalidOrders_AreNotPassedInto_AddOrder()
    {
        SampleOrders.invalidOrders.ForEach(order => {
            OrderController.AddOrder(order);
            
            mockService.Verify(mock => mock.AddOrder(order), Times.Never);
        });
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_OneKit()
    {
        Order order = SampleOrders.kitsNumber1;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 98.99M)));
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_NineKits()
    {
        Order order = SampleOrders.kitsNumber9;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 890.91M)));
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_TenKits()
    {
        Order order = SampleOrders.kitsNumber10;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 940.41M)));
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_FourtyNineKits()
    {
        Order order = SampleOrders.kitsNumber49;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 4607.98M)));
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_FiftyKits()
    {
        Order order = SampleOrders.kitsNumber50;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 4207.08M)));
    }

    [Fact]
    public void AddOrder_PassingCorrectPrice_MaxKits()
    {
        Order order = SampleOrders.kitsNumber999;
       
        OrderController.AddOrder(order);
       
        mockService.Verify(mock => mock.AddOrder(It.Is<Order>(x => x.price == 84057.36M)));
    }
}
