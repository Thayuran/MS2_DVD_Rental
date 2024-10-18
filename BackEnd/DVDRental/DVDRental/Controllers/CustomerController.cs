using DVDRental.DTOs.RequestDTO;
using DVDRental.Entities;
using DVDRental.Repositories;
using DVDRental.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("GET_ALL_CUSTOMERS")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers=await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("GET_CUSTOMER{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("ADD_CUSTOMER")]
        public async Task<IActionResult> CreateCustomer(CustomerRequestDTO customer)
        {
            await _customerService.AddCustomer(customer);
            
            return Ok();
        }

        [HttpPut("UPDATE_CUSTOMER{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, CustomerRequestDTO customer)
        {
            var updatedCustomer = await _customerService.UpdateCustomer(id,customer);
            return Ok(updatedCustomer);
        }

        [HttpDelete("DELETE_CUSTOMER{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateCustomer(string id)
        {
            var customer = await _customerService.ActivateCustomer(id);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCustomer(string id)
        {
            var customer = await _customerService.DeactivateCustomer(id);
            if (customer == null) return NotFound();

            return Ok(customer);
        }


    }
}

