using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;
using DVDRental.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DVDRental.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomers()
        {
            var customers=await _customerRepository.GetAllCustomers();
            return customers.Select(customer => new CustomerResponseDTO
            {
                CustomerId = customer.Id,
                FullName = customer.CustomerName,
                Email=customer.Email,
                Address=customer.Address,
                AddressId=customer.AddressId,
                PhoneNumber=customer.PhoneNo,
                JoinedDate=customer.joined_date,
                Action=customer.Action
               
            }).ToList();
           
        }
        public async Task<CustomerResponseDTO> GetCustomerById(string customerId)
        {
            var selectcustomer = await _customerRepository.GetCustomerById(customerId);

            if (selectcustomer == null) return null;

            return new CustomerResponseDTO
            {
                CustomerId = selectcustomer.Id,
                FullName = selectcustomer.CustomerName,
                Email = selectcustomer.Email,
                Address = selectcustomer.Address,
                AddressId = selectcustomer.AddressId,
                PhoneNumber = selectcustomer.PhoneNo,
                JoinedDate = selectcustomer.joined_date,
                Action = selectcustomer.Action
            };
        }

        public async Task<CustomerResponseDTO> AddCustomer(CustomerRequestDTO customer)
        {
            var newCustomer = new Customer
            {
                CustomerName = customer.FullName,
                Email= customer.Email,
                PhoneNo=customer.PhoneNumber,
                joined_date=DateTime.Now,
                AddressId=customer.AddressId,
                Address=customer.Address
               
            };

            var cus = await _customerRepository.AddCustomer(newCustomer);
            var response = new CustomerResponseDTO
            {
               CustomerId=cus.Id,
               FullName=cus.CustomerName,
               Email=cus.Email,
               Address=cus.Address,
               AddressId=(int)cus.AddressId,
               PhoneNumber=cus.PhoneNo,
               JoinedDate=cus.joined_date,
               Action = cus.Action
            };
            return response;


        }

        public async Task<CustomerResponseDTO> UpdateCustomer(string cusId,CustomerRequestDTO requestcustomer)
        {
            var selectCust = await _customerRepository.GetCustomerById(cusId);

            if (selectCust == null) return null;
            selectCust.CustomerName = requestcustomer.FullName; 
            selectCust.Email=requestcustomer.Email;
            selectCust.Address=requestcustomer.Address;
            selectCust.AddressId= requestcustomer.AddressId;
            selectCust.PhoneNo = requestcustomer.PhoneNumber;
            selectCust.joined_date=DateTime.Now;
            selectCust.Action = selectCust.Action;

             var updateCus=await _customerRepository.UpdateCustomer(selectCust);

            var Cusresponse = new CustomerResponseDTO
            {
                CustomerId = updateCus.Id,
                FullName = updateCus.CustomerName,
                Email = updateCus.Email,
                Address = updateCus.Address,
                AddressId = (int)updateCus.AddressId,
                PhoneNumber = updateCus.PhoneNo,
                JoinedDate = updateCus.joined_date,
                Action = updateCus.Action
            };
            return Cusresponse;
        }

        public async Task<bool> DeleteCustomer(string customerId)
        {
            await _customerRepository.DeleteCustomer(customerId);
            return true;
        }

        public async Task<bool> ActivateCustomer(string id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return false;
            }
            customer.Action = true;
            await _customerRepository.UpdateCustomer(customer);
            return true;
        }
        public async Task<bool> DeactivateCustomer(string customerId)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
            if (customer == null) 
                return false;

            customer.Action = false;
            await _customerRepository.UpdateCustomer(customer);
            return true ;
        }
        public async Task<bool> UpdateCustomerAction(string id, bool action)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return false;
            }
            customer.Action = action;
            await _customerRepository.UpdateCustomer(customer);

            return true;
        }
    }
}
