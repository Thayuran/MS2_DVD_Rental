using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ImovieRepository _movieRepository;

        public MovieController(ImovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
    }
}
