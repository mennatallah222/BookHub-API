using API.Core.Features.Queries.Models;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Handlers
{
    public class CustomerHandler : IRequestHandler<GetCustomerListQuery, List<Customer>>
    {
        public Task<List<Customer>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
