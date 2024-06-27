using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using API.Core.Features.Queries.Responses;

namespace API.Core.Features.Queries.Models
{
    public class GetCustomerListQuery : IRequest<List<GetCustomersResponse>>
    {
    }
}
