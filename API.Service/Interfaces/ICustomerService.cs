using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Service.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAll();
        public Task<Customer> GetByIdAsync(int id);
    }
}
