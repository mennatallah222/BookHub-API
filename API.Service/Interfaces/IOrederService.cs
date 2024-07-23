using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Interfaces
{
    public interface IOrederService
    {
        Task<string> AddOrderAsync(Order o);
        Task<List<Product>> GetAllOrdersAsync();
        Task<Product> GetOrderByIdAsync(int id);
        Task<bool> IsNameExist(string name);
        Task<Product> UpdateOrderAsync(Order o);
        Task<string> SaveOrderChangesAsync();
    }

}
