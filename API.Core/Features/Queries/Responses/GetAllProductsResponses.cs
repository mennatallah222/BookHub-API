using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Responses
{
    public class GetAllProductsResponses
    {
        //public int ProductId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        //public bool IsDeleted { get; set; }
        //public int? OrderId { get; set; }

        public string? CategoryName { get; set; }
        public string? Image { get; set; }


    }
}
