using API.Infrastructure.Infrastructures;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Infrastructure.Interfaces
{
    public interface ICustomer : IGenericRepo<Customer>
    {
        //public Task<List<Customer>> GetCustomerListAsync();
        //public Task<Customer> GetCustomerByID(int id);
    }
}
