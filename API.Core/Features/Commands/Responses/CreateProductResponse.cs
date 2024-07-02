using API.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Commands.Responses
{
    public class CreateProductResponse
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
