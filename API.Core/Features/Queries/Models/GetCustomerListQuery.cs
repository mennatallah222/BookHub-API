using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using API.Core.Features.Queries.Responses;
using API.Core.Bases;

namespace API.Core.Features.Queries.Models
{                                               //vImp
    public class GetCustomerListQuery : IRequest<Response<List<GetCustomersResponse>>>
    {
    }
}
