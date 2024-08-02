using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class CreateOrderCommand : IRequest<string>
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
