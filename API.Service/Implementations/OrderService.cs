using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Implementations
{
    public class OrderService : IOrederService
    {
        private readonly IOrderRepo _repo;
        private readonly ICustomer _customerRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICartRepo _cartRepo;
        private readonly ApplicationDBContext _dbContext;
        public OrderService(IOrderRepo orderRepo, ICustomer customerRepo, IProductRepo productRepo, ICartRepo cartRepo, ApplicationDBContext dBContext)
        {
            _repo = orderRepo;
            _dbContext = dBContext;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }
        public async Task<string> AddOrderAsync(Order o)
        {
            var customer = await _customerRepo.GetCustomerByID(o.Customer.CustomerId);
            var products = new List<Product>();
            foreach (var pid in o.OrderItems)/////////////////////////////////////to be chnanged
            {
                var p = await _productRepo.GetProductByIdAsync(pid.ProductId);

                if (p == null) return $"Product {p.Name} is not found!";
                products.Add(p);

            }
            var order = new Order
            {
                CustomerId = customer.CustomerId,
                OrderItems = o.OrderItems,
                PaymentMethod = o.PaymentMethod,
                Status = "Pending",
                ShippingAddress = customer.Address,
                PaymentStatus = o.PaymentStatus,
                OrderDate = DateTime.UtcNow,
                TrackingNumber = ""
            };
            await _repo.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return "Oreder created successfully!";
        }

        public Task<List<Product>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameExist(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveOrderChangesAsync()
        {
            await _repo.SaveChangesAsync();
            return "done";
        }

        public Task<Product> UpdateOrderAsync(Order o)
        {
            throw new NotImplementedException();
        }
    }
}
