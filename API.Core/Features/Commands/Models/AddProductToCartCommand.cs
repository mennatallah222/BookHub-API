using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class AddProductToCartCommand : IRequest<string>
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
