using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestResponseDTO>>> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpPost]
        public async Task<ActionResult<RequestResponseDTO>> CreateRequest(RequestDTO requestDto)
        {
            var id = await _requestService.AddRequestAsync(requestDto);
            return CreatedAtAction(nameof(GetAllRequests), new { id = id }, id);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateRequestStatus(string id, [FromBody] string status)
        {
            await _requestService.UpdateRequestStatusAsync(id, status);
            return NoContent();
        }
    }
}
