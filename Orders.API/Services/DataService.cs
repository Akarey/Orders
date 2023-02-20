using System.Text.Json;

namespace Orders.API.Services
{
    public class DataService : IDataService
    {
        static readonly object locker = new object();

        public IWebHostEnvironment WebHostEnvironment { get; }

        public DataService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "orders.json"); }
        }

        public string GetOrdersJsonString()
        {
            lock (locker)
            {
                return File.ReadAllText(JsonFileName);
            }
        }

        public List<Order> GetOrders()
        {
            string content = GetOrdersJsonString();
            try
            {
                return JsonSerializer.Deserialize<List<Order>>(content);
            }
            catch (JsonException)
            {
                return new List<Order>();
            }
        }

        public void AddOrder(Order order)
        {
            lock (locker)
            {
                List<Order> orders = GetOrders();

                orders.Add(order);

                using (FileStream outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<Order>>(outputStream, orders,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true
                    });
                };
            }
        }
    }
}

