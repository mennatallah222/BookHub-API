using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Interfaces
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        public Task<List<Product>> GetProductListAsync();
        public Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProduct(Product product);
        Task SaveChangesAsync();

    }
}
