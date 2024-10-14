using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
