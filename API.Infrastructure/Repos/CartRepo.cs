using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repos
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        private readonly DbSet<Cart> _carts;
        private readonly ApplicationDBContext _dBContext;
        public CartRepo(ApplicationDBContext dBContext) : base(dBContext)
        {
            _carts = dBContext.Set<Cart>();
            _dBContext = dBContext;
        }
        public async Task<Cart> AddAsync(Cart entity)
        {
            _dBContext.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public Task AddRangeAsync(ICollection<Product> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByIDAsync(int cid)
        {
            return await _carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.UserId == cid);
        }

        public async Task UpdateAsync(Cart entity)
        {
            _carts.Update(entity);
            _dbContext.SaveChanges();

        }

        public async Task ClearCartAsync(int customerID)
        {
            var cart = await GetCartByIDAsync(customerID);
            if (cart != null)
            {
                _dbContext.CartItems.RemoveRange(cart.CartItems);
                _dbContext.SaveChanges();
            }
        }
    }
}
