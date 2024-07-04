using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;

namespace API.Core.Features.Queries.Responses
{
    public class GetCartResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalAmount => CartItems.Sum(item => item.TotalAmount);

    }
}
