using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentService _rentalService;

        public RentalsController(IRentService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DVD_RentalDTO>>> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return Ok(rentals);
        }

        [HttpPost("from-request/{requestId}")]
        public async Task<ActionResult<RentalResponseDTO>> CreateRentalFromRequest(string requestId)
        {
            var id = await _rentalService.CreateRentalFromRequestAsync(requestId);
            return CreatedAtAction(nameof(GetAllRentals), new { id = id }, id);
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnDVD(string id, [FromQuery] bool isDamaged)
        {
            await _rentalService.ReturnDVDAsync(id, isDamaged);
            return NoContent();
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<RentalResponseDTO>>> GetOverdueRentals()
        {
            var overdueRentals = await _rentalService.GetOverdueRentalsAsync();
            return Ok(overdueRentals);
        }

        [HttpPost("calculate-fines")]
        public async Task<IActionResult> CalculateAndApplyFines()
        {
            await _rentalService.CalculateAndApplyFinesAsync();
            return NoContent();
        }
    }
}
