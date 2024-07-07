using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Infrastructure.Interfaces
{
    public interface ICartRepo : IGenericRepo<Cart>
    {
        Task AddRangeAsync(ICollection<Product> entities);
        Task<Cart> GetCartByIDAsync(int id);
        Task UpdateAsync(Cart entity);
        Task ClearCartAsync(int customerID);
    }
}
