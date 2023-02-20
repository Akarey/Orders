using Orders.API;

namespace Orders.UnitTests;

public static class SampleOrders
{
    public static readonly Order genericOrder = new Order
    {
        customerId = 3,
        kitsNumber = 4,
        deliveryDate = new DateOnly(2023, 05, 22)
    };

    public static readonly Order genericOrder2 = new Order
    {
        customerId = 5,
        kitsNumber = 6,
        deliveryDate = new DateOnly(2023, 07, 11)
    };

    public static readonly Order genericOrder3 = new Order
    {
        customerId = 11,
        kitsNumber = 68,
        deliveryDate = new DateOnly(2024, 01, 01)
    };

    public static readonly Order pricedOrder = new Order
    {
        customerId = 12,
        kitsNumber = 14,
        deliveryDate = new DateOnly(2023, 09, 12),
        price = 1316.57M
    };

    public static readonly Order pricedOrder2 = new Order
    {
        customerId = 234,
        kitsNumber = 3,
        deliveryDate = new DateOnly(2023, 06, 08),
        price = 296.97M
    };

    public static readonly Order pricedOrder3 = new Order
    {
        customerId = 1,
        kitsNumber = 120,
        deliveryDate = new DateOnly(2023, 07, 20),
        price = 10096.98M
    };

    public static readonly Order kitsNumberNegative = new Order
    {
        customerId = 11,
        kitsNumber = -3,
        deliveryDate = new DateOnly(2024, 01, 01)
    };

    public static readonly Order kitsNumberZero = new Order
    {
        customerId = 11,
        kitsNumber = 0,
        deliveryDate = new DateOnly(2024, 01, 01)
    };

    public static readonly Order kitsNumberTooBig = new Order
    {
        customerId = 11,
        kitsNumber = 1140,
        deliveryDate = new DateOnly(2024, 01, 01)
    };


    public static readonly Order deliveryDateToday = new Order
    {
        customerId = 11,
        kitsNumber = 68,
        deliveryDate = DateOnly.FromDateTime(DateTime.Now)
    };

    public static readonly Order deliveryDateInThePast = new Order
    {
        customerId = 11,
        kitsNumber = 68,
        deliveryDate = new DateOnly(2022, 11, 25)
    };

    public static readonly Order kitsNumber1 = new Order
    {
        customerId = 1,
        kitsNumber = 1,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly Order kitsNumber9 = new Order
    {
        customerId = 1,
        kitsNumber = 9,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly Order kitsNumber10 = new Order
    {
        customerId = 1,
        kitsNumber = 10,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly Order kitsNumber49 = new Order
    {
        customerId = 1,
        kitsNumber = 49,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly Order kitsNumber50 = new Order
    {
        customerId = 1,
        kitsNumber = 50,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly Order kitsNumber999 = new Order
    {
        customerId = 1,
        kitsNumber = 999,
        deliveryDate = new DateOnly(2024, 01, 07)
    };

    public static readonly List<Order> validOrders = new List<Order>
    {
        genericOrder,
        genericOrder2,
        genericOrder3
    };

    public static readonly List<Order> pricedOrders = new List<Order>
    {
        pricedOrder,
        pricedOrder2,
        pricedOrder3
    };

    public static readonly List<Order> invalidOrders = new List<Order>
    {
        kitsNumberNegative,
        kitsNumberTooBig,
        kitsNumberZero,
        deliveryDateToday,
        deliveryDateInThePast
    };

}