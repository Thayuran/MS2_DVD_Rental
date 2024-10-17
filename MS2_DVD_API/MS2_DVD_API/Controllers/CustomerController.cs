using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet("GET_ALL_CUSTOMERS")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("GET_CUSTOMER{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("ADD_CUSTOMER")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            var createdCustomer = await _customerRepository.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("UPDATE_CUSTOMER{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId) return BadRequest();
            var updatedCustomer = await _customerRepository.UpdateCustomer(customer);
            return Ok(updatedCustomer);
        }

        [HttpDelete("DELETE_CUSTOMER{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerRepository.DeleteCustomer(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null) return NotFound();

            customer.Action = true;
            await _customerRepository.UpdateCustomer(customer);
            return Ok(customer);
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null) return NotFound();

            customer.Action = false;
            await _customerRepository.UpdateCustomer(customer);
            return Ok(customer);
        }


    }
}
