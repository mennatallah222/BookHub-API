using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public string? Image { get; set; }
    }
}
