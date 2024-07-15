using System.ComponentModel.DataAnnotations;

namespace API.Core.Features.Commands.Responses
{
    public class CreateBookResponse
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? Image { get; set; }
    }
}
