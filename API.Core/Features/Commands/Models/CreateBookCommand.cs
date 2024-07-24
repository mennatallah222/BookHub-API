using API.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.Core.Features.Commands.Models
{
    public class CreateBookCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public List<string>? GenresNames { get; set; }
        public IFormFile? Image { get; set; }

    }
}
