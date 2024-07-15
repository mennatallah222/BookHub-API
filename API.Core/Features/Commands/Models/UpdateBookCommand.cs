using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Commands.Models
{
    public class UpdateBookCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Author { get; set; }
        public int PagesNumber { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public List<string> BookGenres { get; set; }
        public string? Image { get; set; }
    }
}
