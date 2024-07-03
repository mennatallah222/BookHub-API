using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Core.Features.Queries.Responses
{
    public class GetCustomersResponse
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string? OrderStatus { get; set; }
        public List<OrderDTOs>? Orders { get; set; }
    }
}
