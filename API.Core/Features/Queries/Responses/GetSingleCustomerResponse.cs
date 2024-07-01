using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Responses
{
    public class GetSingleCustomerResponse
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string? OrderStatus { get; set; }
        public List<String>? Products { get; set; }
    }
}
