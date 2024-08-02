using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Service.Implementations
{
    public class OrderService : IOrederService
    {
        private readonly IOrderRepo _repo;
        private readonly IProductRepo _productRepo;
        private readonly ICartRepo _cartRepo;
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<User> _userManager;

        public OrderService(IOrderRepo orderRepo,
                            IProductRepo productRepo,
                            ICartRepo cartRepo,
                            UserManager<User> userManager,
                            ApplicationDBContext dBContext)
        {
            _repo = orderRepo;
            _dbContext = dBContext;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _userManager = userManager;
        }
        public async Task<string> AddOrderAsync(Order o)
        {
            var user = await _userManager.FindByIdAsync(o.User.Id.ToString());
            var products = new List<Product>();
            foreach (var pid in o.OrderItems)
            {
                var p = await _productRepo.GetProductByIdAsync(pid.ProductId);

                if (p == null) return $"Product {p.Name} is not found!";
                products.Add(p);

            }
            var order = new Order
            {
                UserId = user.Id,
                OrderItems = o.OrderItems,
                PaymentMethod = o.PaymentMethod,
                Status = "Pending",
                ShippingAddress = user.Address,
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
