using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Enums;

namespace API.Service.Interfaces
{
    public interface IProductsService
    {
        Task<string> AddProductAsync(Product product, List<string> genreNames);


        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> IsNameExist(string name);
        Task<string> UpdateProductAsync(Product p, List<string> genreNames);
        IQueryable<Product> GetProductsQueryable();
        IQueryable<Product> FilterProductPaginationQueryable(ProductOrderingEnum orderingEnum, string search);

        public Task<string> DeleteProductAsync(int productId);

    }
}
