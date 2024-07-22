using API.Infrastructure.Interfaces;
using API.Service.Interfaces;

namespace API.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomer _customerRepo;
        public CustomerService(ICustomer customerRepo)
        {
            _customerRepo = customerRepo;
        }
        //public async Task<List<Customer>> GetAll()
        //{
        //   return await _customerRepo.GetCustomerListAsync();
        //}

        //public async Task<Customer> GetByIdAsync(int id)
        //{
        //    var customer = await _customerRepo
        //                        .GetCustomerByID(id);
        //    return customer;
        //}
    }
}
