using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repos
{
    public class CustomerRepo : ICustomer
    {
        private readonly ApplicationDBContext _dBContext;
        public CustomerRepo(ApplicationDBContext dBContext) 
        {
            _dBContext= dBContext;
        }
        public async Task<List<Customer>> GetCustomerListAsync()
        {
            return await _dBContext.Customers.ToListAsync();
        }
    }
}
