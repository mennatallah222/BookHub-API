using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;

namespace API.Core.Features.Queries.Responses
{
    public class GetOrdersHistoryResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        // public string? OrderStatus { get; set; }
        public List<OrderDTOs>? Orders { get; set; }
    }
}
