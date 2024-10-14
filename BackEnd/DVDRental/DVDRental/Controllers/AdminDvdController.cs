using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDvdController : ControllerBase
    {
        [HttpGet]
        public Task CreateNewDvd()
        {
            return Task.CompletedTask;
        }
    }
}
