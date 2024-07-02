using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IProductsService
    {
        Task<string> AddProductAsync(Product p);
        Task<List<Product>> GetAllProductsAsync();
         Task<Product> GetProductByIdAsync(int id);
    }
}
