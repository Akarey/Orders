using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Orders.API;
using Orders.API.Services;
using System.Text.Json;

namespace Orders.UnitTests;

public class DataServiceTests : XunitContextBase
{
    private DataService DataService;

    private string mockPath;

    public DataServiceTests(ITestOutputHelper helper) : base(helper)
    {
        Mock<IWebHostEnvironment> mockEnvironment = new Mock<IWebHostEnvironment>();
    
        mockPath = Path.Combine(Environment.CurrentDirectory, Context.Test.DisplayName);
        string dirPath = Path.Combine(mockPath, "data");
        string fullPath = Path.Combine(dirPath, "orders.json");

        mockEnvironment.Setup(m => m.WebRootPath).Returns(mockPath);

        DataService = new DataService(mockEnvironment.Object);
        Directory.CreateDirectory(dirPath);
        using (FileStream fs = File.Create(Path.Combine(fullPath)));
    }

    public override void Dispose()
    {
        Directory.Delete(mockPath, true);
    }


    [Fact]
    public void GetOrders_WithNoOrders_IsEmpty()
    {
        List<Order> orders = DataService.GetOrders();

        orders.Should().BeEmpty();
    }

    [Fact]
    public void AddOrder_ThenGetOrders_RetunsThatOneOrder()
    {
        Order order = SampleOrders.pricedOrder;
        DataService.AddOrder(order);

        List<Order> orders = DataService.GetOrders();

        orders.Count.Should().Be(1);

        Order fetchedOrder = orders.First();

        AreEquivalent(fetchedOrder, order).Should().BeTrue();
    }

    [Fact]
    public void ThreeOrders_Simultaneous()
    {
        SampleOrders.pricedOrders.ForEach(x => DataService.AddOrder(x));

        List<Order> resultingOrders = DataService.GetOrders();

        resultingOrders.Count.Should().Be(3);

        foreach (Tuple<Order, Order> tuple in SampleOrders.pricedOrders.Zip(resultingOrders, Tuple.Create))
        {
            AreEquivalent(tuple.Item1, tuple.Item2).Should().BeTrue();
        }
    }

    [Fact]
    public void GetOrders_CorrespondsTo_GetOrdersJsonString()
    {
        SampleOrders.pricedOrders.ForEach(order => 
        {
            DataService.AddOrder(order);
        });


        List<Order> returnedOrders = DataService.GetOrders();
        string returnedJsonString = DataService.GetOrdersJsonString();

        string expectedJson = JsonSerializer.Serialize(returnedOrders, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });
        returnedJsonString.Should().Be(expectedJson);

        List<Order> expectedOrders = JsonSerializer.Deserialize<List<Order>>(returnedJsonString);

        expectedOrders.Should().NotBeNull();
        returnedOrders.Count.Should().Be(expectedOrders.Count);

        foreach (Tuple<Order, Order> tuple in returnedOrders.Zip(expectedOrders, Tuple.Create))
        {
            AreEquivalent(tuple.Item1, tuple.Item2).Should().BeTrue();
        }
    }

    [Fact]
    public void DeliveryDate_IsStored_WithoutTime()
    {
        Order order = SampleOrders.pricedOrder2;

        DataService.AddOrder(order);

        string jsonString = DataService.GetOrdersJsonString();

        jsonString.Should().Contain("\"2023-06-08\"");
    }

    private bool AreEquivalent(Order order1, Order order2)
    {
        return order1.kitsNumber == order2.kitsNumber &&
               order1.price == order2.price &&
               order1.customerId == order2.customerId &&
               order1.deliveryDate == order2.deliveryDate;
    }
}
