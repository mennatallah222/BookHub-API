using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomer _customerRepo;
        public CustomerService(ICustomer customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<List<Customer>> GetAll()
        {
           return await _customerRepo.GetCustomerListAsync();
        }
    }
}
