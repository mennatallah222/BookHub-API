using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Features.Queries.Models
{
    public class GetCustomerListQuery : IRequest<List<Customer>>
    {
    }
}
