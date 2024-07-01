using API.Core.Bases;
using API.Core.Features.Queries.Responses;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Models
{
    public class GetCustomerByID:IRequest<Response<GetSingleCustomerResponse>>
    {
        public int Id { get; set; }
    }
}
