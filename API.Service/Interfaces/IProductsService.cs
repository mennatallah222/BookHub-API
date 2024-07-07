using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;

namespace API.Service.Interfaces
{
    public interface IProductsService
    {
        Task<string> AddProductAsync(Product p);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> IsNameExist(string name);
        Task<Product> UpdateProductAsync(Product p);
        IQueryable<Product> GetProductsQueryable();
        IQueryable<Product> FilterProductPaginationQueryable(ProductOrderingEnum orderingEnum, string search);
    }
}
