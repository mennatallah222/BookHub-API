using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Interfaces
{
    public interface ICartService
    {
        public Task<Cart> GetCartContent(int id);
        public Task<Cart> AddCart(Cart cart);
        public void UpdateCart(Cart cart);
    }
}
