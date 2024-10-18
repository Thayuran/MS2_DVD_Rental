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
    public class AdminDvdController : ControllerBase
    {
       /* private readonly IAdminDvdRepository _adminDvdRepository;*/
        private readonly IAdminDvdService _adminDvdService;

        public AdminDvdController(IAdminDvdService adminDvdService)
        {
            _adminDvdService = adminDvdService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dvds= _adminDvdService.GetAllDVDsAsync();
            if (dvds == null)
                return NotFound();
            return Ok(dvds);
        }

        [HttpGet("{dvdId}")]
        public IActionResult GetById(string Id)
        {
            var dvd = _adminDvdService.GetDVDByIdAsync(Id);
            if (dvd == null)
                return NotFound();
            return Ok(dvd);
        }

        [HttpPost]
        public IActionResult CreateNewDVD(DVDRequestDTO movie)
        {
            _adminDvdService.AddDVDAsync(movie);
            return Ok();
        }
    }
}
