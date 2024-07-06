using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class CreateOrderCommand : IRequest<string>
    {
        //public int OrderId { get; set; }
        public int CustomerID { get; set; }
        public string ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        //i can add a "confirm?" attribute --> bool
    }
}
