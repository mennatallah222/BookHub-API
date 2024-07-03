using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Infrastructure.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<Order> AddAsync(Order entity);

        public Task<List<Order>> GetListAsync();
        public Task<Order> GetByIdAsync(int id);
        // Task UpdateAsync(Order order);
        Task SaveChangesAsync();
    }
}
