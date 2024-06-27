using API.Infrastructure.Data;
using API.Infrastructure.Infrastructures;
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
    public class CustomerRepo :GenericRepo<Customer>, ICustomer
    {
        private readonly DbSet<Customer> _customers;
        public CustomerRepo(ApplicationDBContext dBContext) :base(dBContext) 
        {
            _customers= dBContext.Set<Customer>();
        }
        public async Task<List<Customer>> GetCustomerListAsync()
        {                                                        //eagerly loading products, when fetching customers
            return await _customers.Include(x=>x.Orders).ThenInclude(o=>o.Products).ToListAsync();
        }
    }
}
