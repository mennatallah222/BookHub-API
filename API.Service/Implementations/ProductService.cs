using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    public class ProductService : IProductsService
    {
        private readonly IProductRepo _repo;
        private readonly ApplicationDBContext _dbContext;
        public ProductService(IProductRepo productRepo, ApplicationDBContext dBContext)
        {
            _repo = productRepo;
            _dbContext = dBContext;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.GetProductListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var products = await _repo.GetProductByIdAsync(id);

            return products;
        }

        public async Task<string> AddProductAsync(Product product)
        {
            var existingCategory = await _dbContext.Categories
                                        .FirstOrDefaultAsync(c => c.Name == product.Category.Name);
            if (existingCategory != null)
            {
                product.Category = existingCategory;
            }
            else
            {
                _dbContext.Categories.Add(product.Category);
            }
            var prod = await _repo.GetTableNoTrasking().FirstOrDefaultAsync(x => x.Name.Equals(product.Name));

            if (prod != null)
            {
                return "Product already exists!";
            }
            await _repo.AddProduct(product);
            await _dbContext.SaveChangesAsync();
            return "Success";

        }
        public async Task<Product> UpdateProductAsync(Product p)
        {

            await _repo.UpdateAsync(p);
            await _dbContext.SaveChangesAsync();
            return p;
        }
        public async Task<bool> IsNameExist(string name)
        {
            var res = _repo.GetTableNoTrasking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (res != null) return true;
            return false;
        }


    }
}
