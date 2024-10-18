using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;

namespace DVDRental.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponseDTO> AddCustomer(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomers();
        Task<CustomerResponseDTO> GetCustomerById(string customerId);
        Task<CustomerResponseDTO> UpdateCustomer(string id,CustomerRequestDTO customer);
        Task<bool> DeleteCustomer(string customerId);
        Task<bool> ActivateCustomer(string id);
        Task<bool> DeactivateCustomer(string customerId);
        Task<bool> UpdateCustomerAction(string id, bool action);
    }
}
