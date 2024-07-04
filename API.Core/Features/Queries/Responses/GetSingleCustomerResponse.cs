using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;

namespace API.Core.Features.Queries.Responses
{
    public class GetSingleCustomerResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<CartItemDto>? CartItems { get; set; }
    }
}
