
namespace Orders.API.Services
{
    public interface IDataService
    {
        void AddOrder(Order order);
        List<Order> GetOrders();
        string GetOrdersJsonString();
    }
}