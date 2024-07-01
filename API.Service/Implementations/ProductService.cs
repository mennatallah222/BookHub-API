using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Implementations
{
    public class ProductService : IProductsService
    {
        private readonly IProductRepo _repo;
        public ProductService(IProductRepo productRepo)
        {
            _repo = productRepo;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.GetProductListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var products=await _repo.GetTableNoTrasking()
                                    .Include(r=>r.Reviews.ToList())
                                    .Include(cn=>cn.Category.Name)
                                    .FirstOrDefaultAsync(x=>x.ProductId==id);
            return products;
        }
    }
}
