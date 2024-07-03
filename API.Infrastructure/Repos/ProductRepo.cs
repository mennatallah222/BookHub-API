using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repos
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly DbSet<Product> _products;
        private readonly ApplicationDBContext _dbContext;
        public ProductRepo(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
            _products = dBContext.Set<Product>();
        }

        public async Task<Product> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include(p => p.Reviews)
                                  .Include(p => p.Category)
                                  .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _products.Include(cn => cn.Category).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByNames(List<string> names)
        {
            return await _dbContext.Products.Where(p => names.Contains(p.Name)).ToListAsync();
        }

        public async Task UpadteRangeAsync(List<Product> entities)
        {
            _products.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByIDS(List<int> entities)
        {
            using (var context = new ApplicationDBContext())
            {
                return await _products.Where(p => entities.Contains(p.ProductId))
            .ToListAsync();
            }
        }
    }
}
