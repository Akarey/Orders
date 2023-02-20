using Microsoft.AspNetCore.Mvc;
using Orders.API.Services;

namespace Orders.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private const decimal basePrice = 98.99M;
    public IDataService DataService;
    public OrderController(IDataService dataService)
    {
        DataService = dataService;
    }


    [HttpGet(Name = "ListOrders")]
    public IActionResult ListOrders()
    {
        string jsonString = DataService.GetOrdersJsonString();

        return Ok(jsonString);
    }

    [HttpPost(Name = "AddOrder")]
    public IActionResult AddOrder([FromBody] Order order)
    {
        if (IsValid(order))
        {
            order.price = DeterminePrice(order.kitsNumber);

            DataService.AddOrder(order);
            
            return Ok("Success");
        }
        else
        {
            return this.BadRequest();
        }

    }

    private bool IsValid(Order order)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        return order.kitsNumber > 0 && order.kitsNumber < 1000 && 
               order.deliveryDate.CompareTo(currentDate) > 0;
    }

    public decimal DeterminePrice(int kitsNumber)
    {
        decimal discountCoefficient = 1.00M;
        if (kitsNumber >= 50)
        {
            discountCoefficient = 0.85M;
        }
        else if (kitsNumber >= 10)
        {
            discountCoefficient = 0.95M;
        }

        return decimal.Round(kitsNumber * basePrice * discountCoefficient, 2, MidpointRounding.AwayFromZero);
    }
}

