namespace Orders.API;

public class Order
{
    public int customerId { get; set; }
    public int kitsNumber { get; set; }
    public DateOnly deliveryDate { get; set; }
    public decimal price { get; set; }
}