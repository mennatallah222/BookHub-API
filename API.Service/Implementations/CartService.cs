using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _carRepo;
        public CartService(ICartRepo cartRepo)
        {
            _carRepo = cartRepo;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            return await _carRepo.AddAsync(cart);
        }

        public async Task<Cart> GetCartContent(int cid)
        {
            return await _carRepo.GetCartByIDAsync(cid);
        }

        public async void UpdateCart(Cart cart)
        {
            await _carRepo.UpdateAsync(cart);
        }
    }
}
