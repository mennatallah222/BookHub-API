using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repos
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly DbSet<Product> _products;
        public ProductRepo(ApplicationDBContext dBContext) : base(dBContext)
        {
            _products = dBContext.Set<Product>();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _products.ToListAsync();
        }
    }
}
