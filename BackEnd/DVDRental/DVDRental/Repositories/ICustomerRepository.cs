using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(string customerId);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(string customerId);
        Task<bool> ActivateCustomer(string id);
        Task<bool> DeactivateCustomer(string customerId);
        Task<bool> UpdateCustomerAction(string id, bool action);
    }
}
