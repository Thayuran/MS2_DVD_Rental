using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.IService
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse> GetCustomerById(int customerId);
        Task<CustomerResponse> AddCustomer(CustomerRequest customer);
        Task<CustomerResponse> UpdateCustomer(int customerId, CustomerRequest customer);
        Task<bool> DeleteCustomer(int customerId);
        Task<bool> ActivateCustomer(int customerId);
        Task<bool> DeactivateCustomer(int customerId);
        Task<bool> UpdateCustomerAction(int customerId, bool action);
    }
}
