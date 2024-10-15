using DVDRental.Entities;
using DVDRental.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDvdController : ControllerBase
    {
        private readonly IAdminDvdRepository _adminDvdRepository;

        public AdminDvdController(IAdminDvdRepository adminDvdRepository)
        {
            _adminDvdRepository = adminDvdRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dvds=_adminDvdRepository.GetAllDVDs();
            return Ok(dvds);
        }

        [HttpGet("{dvdId}")]
        public IActionResult GetById(string Id)
        {
            var dvd = _adminDvdRepository.GetMovieById(Id);
            if (dvd == null)
                return NotFound();
            return Ok(dvd);
        }

        [HttpPost]
        public IActionResult CreateNewDVD(MovieDvd movie)
        {
            var mov = new MovieDvd
            {
                ID=movie.ID,
                MovieName=movie.MovieName,
                Director=movie.Director,
                Categories=movie.Categories,
                ReleaseDate=movie.ReleaseDate,
                Copies=movie.Copies
               
            };
            _adminDvdRepository.AddDVD(mov);
            return Ok();
        }
    }
}
