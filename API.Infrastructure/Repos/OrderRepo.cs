using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repos
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly ApplicationDBContext _dbContext;
        public OrderRepo(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
            _orders = dBContext.Set<Order>();
        }

        public async Task<Order> AddAsync(Order entity)
        {
            _orders.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Order>> GetListAsync()
        {
            return await _orders.Include(x => x.Customer).Include(s => s.OrderItems).ToListAsync();
        }
        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orders.Include(x => x.OrderItems).FirstOrDefaultAsync(i => i.OrderId.Equals(id));
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
