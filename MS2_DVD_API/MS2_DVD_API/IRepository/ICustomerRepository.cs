using MS2_DVD_API.Entity;

namespace MS2_DVD_API.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int customerId);
        Task<bool> ActivateCustomer(int customerId);
        Task<bool> DeactivateCustomer(int customerId);
        Task<bool> UpdateCustomerAction(int customerId, bool action);
    
    }
}
