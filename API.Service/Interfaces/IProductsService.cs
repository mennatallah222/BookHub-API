using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Interfaces
{
    public interface IProductsService
    {
        Task<string> AddProductAsync(Product p);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> IsNameExist(string name);
    }
}
