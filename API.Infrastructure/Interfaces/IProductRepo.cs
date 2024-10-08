﻿using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Infrastructure.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        public Task<List<Product>> GetProductListAsync();
        public Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProduct(Product product);
        Task SaveChangesAsync();
        Task UpdateAsync(Product product);
        Task<List<Product>> GetProductsByNames(List<string> products);

        Task UpadteRangeAsync(List<Product> entities);
        Task<List<Product>> GetProductsByIDS(List<int> entities);

    }
}
