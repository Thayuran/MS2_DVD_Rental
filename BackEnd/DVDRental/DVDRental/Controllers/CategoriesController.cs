using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCategory()
        {
            try 
            {
                return Ok();

            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
