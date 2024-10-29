using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerResponse>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return customers.Select(customer => new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                Address = customer.Address,
                PhoneNumber = customer.phoneNumber,
                JoinedDate = customer.joined_date,
                Action = customer.Action
            }).ToList();
        }

        public async Task<CustomerResponse> GetCustomerById(int customerId)
        {
            var selectCustomer = await _customerRepository.GetCustomerById(customerId);

            if (selectCustomer == null) return null;

            return new CustomerResponse
            {
                CustomerId = selectCustomer.CustomerId,
                FullName = selectCustomer.FullName,
                Email = selectCustomer.Email,
                Address = selectCustomer.Address,
                PhoneNumber = selectCustomer.phoneNumber,
                JoinedDate = selectCustomer.joined_date,
                Action = selectCustomer.Action
            };
        }

        public async Task<CustomerResponse> AddCustomer(CustomerRequest customer)
        {
            var newCustomer = new Customer
            {
                FullName = customer.FullName,
                Email = customer.Email,
                phoneNumber = customer.PhoneNumber,
                joined_date = DateTime.Now,
                Address = customer.Address,
                Action = true // Assuming customer is active when added
            };

            var addedCustomer = await _customerRepository.AddCustomer(newCustomer);
            return new CustomerResponse
            {
                CustomerId = addedCustomer.CustomerId,
                FullName = addedCustomer.FullName,
                Email = addedCustomer.Email,
                Address = addedCustomer.Address,
                PhoneNumber = addedCustomer.phoneNumber,
                JoinedDate = addedCustomer.joined_date,
                Action = addedCustomer.Action
            };
        }

        public async Task<CustomerResponse> UpdateCustomer(int customerId, CustomerRequest requestCustomer)
        {
            var selectCustomer = await _customerRepository.GetCustomerById(customerId);

            if (selectCustomer == null) return null;

            selectCustomer.FullName = requestCustomer.FullName;
            selectCustomer.Email = requestCustomer.Email;
            selectCustomer.Address = requestCustomer.Address;
            selectCustomer.phoneNumber = requestCustomer.PhoneNumber;
            selectCustomer.joined_date = DateTime.Now;

            var updatedCustomer = await _customerRepository.UpdateCustomer(selectCustomer);

            return new CustomerResponse
            {
                CustomerId = updatedCustomer.CustomerId,
                FullName = updatedCustomer.FullName,
                Email = updatedCustomer.Email,
                Address = updatedCustomer.Address,
                PhoneNumber = updatedCustomer.phoneNumber,
                JoinedDate = updatedCustomer.joined_date,
                Action = updatedCustomer.Action
            };
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            return await _customerRepository.DeleteCustomer(customerId);
        }

        public async Task<bool> ActivateCustomer(int customerId)
        {
            return await _customerRepository.ActivateCustomer(customerId);
        }

        public async Task<bool> DeactivateCustomer(int customerId)
        {
            return await _customerRepository.DeactivateCustomer(customerId);
        }

        public async Task<bool> UpdateCustomerAction(int customerId, bool action)
        {
            return await _customerRepository.UpdateCustomerAction(customerId, action);
        }
    }
}
}
