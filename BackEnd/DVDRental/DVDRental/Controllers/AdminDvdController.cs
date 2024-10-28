using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
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

        //login credential
        [HttpPost("login")]
        /*public IActionResult AdminLogin(AdminCredentials adminCredentials)
        {
            var result = _adminDvdService.Login(adminCredentials);
            if (result)
                return Ok("Login Successful");
            return Unauthorized("Invalid Credentials");
        }*/






        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dvds=await _adminDvdService.GetAllDVDsAsync();
            if (dvds == null)
                return NotFound();
            return Ok(dvds);
        }

        [HttpGet("dvdId")]
        public async Task<IActionResult> GetById(string Id)
        {
            var dvd = await _adminDvdService.GetDVDByIdAsync(Id);
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



        //categories
       /* [HttpGet("category/{categoryId}")]*/
       /* public async Task<ActionResult<List<DVDResponseDTO>>> GetDVDsByCategory(int categoryId)
        {
            var dvds = await _adminDvdService.GetDVDsByCategoryAsync(categoryId);
            return Ok(dvds);
        }*/
/*
        [HttpPost("{dvdId}/categories/{categoryId}")]
        public async Task<IActionResult> AddDVDToCategory(string dvdId, int categoryId)
        {
            await _adminDvdService.AddDVDToCategoryAsync(dvdId, categoryId);
            return NoContent();
        }*/

        /*[HttpDelete("{dvdId}/categories/{categoryId}")]
        public async Task<IActionResult> RemoveDVDFromCategory(string dvdId, int categoryId)
        {
            await _adminDvdService.RemoveDVDFromCategoryAsync(dvdId, categoryId);
            return NoContent();
        }*/
    }
}
