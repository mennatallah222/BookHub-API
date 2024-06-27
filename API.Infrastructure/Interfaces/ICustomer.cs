using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Interfaces
{
    public interface ICustomer : IGenericRepo<Customer>
    {
        public Task<List<Customer>> GetCustomerListAsync();
    }
}
