using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class CreateOrderCommand : IRequest<string>
    {
        //public int OrderId { get; set; }
        public int CustomerID { get; set; }
        public string ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }

        public List<OrderProductDto> Products { get; set; }
    }
}
