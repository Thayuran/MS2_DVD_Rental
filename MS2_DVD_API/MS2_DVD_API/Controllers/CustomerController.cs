using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Controllers
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
        [HttpGet]
        public async Task<ActionResult<List<CustomerResponse>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> AddCustomer(CustomerRequest customerRequest)
        {
            var customer = await _customerService.AddCustomer(customerRequest);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponse>> UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            var updatedCustomer = await _customerService.UpdateCustomer(id, customerRequest);
            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomer(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateCustomer(int id)
        {
            var activated = await _customerService.ActivateCustomer(id);
            if (!activated)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCustomer(int id)
        {
            var deactivated = await _customerService.DeactivateCustomer(id);
            if (!deactivated)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/action")]
        public async Task<IActionResult> UpdateCustomerAction(int id, [FromBody] bool action)
        {
            var updated = await _customerService.UpdateCustomerAction(id, action);
            if (!updated)
                return NotFound();

            return NoContent();
        }


    }
}
