using API.Core.Bases;
using API.Core.Features.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Models
{
    public class GetAllProductsQuery:IRequest<Response<List<GetAllProductsResponses>>>
    {
    }
}
