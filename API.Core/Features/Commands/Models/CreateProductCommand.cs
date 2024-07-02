using MediatR;
using API.Core.Bases;

namespace API.Core.Features.Commands.Models
{
    public class CreateProductCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public string? Image { get; set; }
        /*  public decimal Price { get; set; }
          public string Name { get; set; }
          public int Quantity { get; set; }
          public string Description { get; set; }
          public string Image { get; set; }
          public string CategoryName { get; set; }*/
    }
}
