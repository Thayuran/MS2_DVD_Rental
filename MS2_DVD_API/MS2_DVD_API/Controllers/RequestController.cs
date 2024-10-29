using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestRespone>>> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestRespone>> GetRequestById(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<RequestRespone>> AddRequest(RequestRespone requestRequest)
        {
            var createdRequest = await _requestService.AddRequestAsync(requestRequest);
            return CreatedAtAction(nameof(GetRequestById), new { id = createdRequest.Id }, createdRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRequest(int id, RequestRespone requestRequest)
        {
            try
            {
                await _requestService.UpdateRequestAsync(id, requestRequest);
                return NoContent(); // Return 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Return 404 Not Found if the request does not exist
            }
        }

        [HttpPost("{id}/activate")]
        public async Task<ActionResult> ActivateRequest(int id)
        {
            var activated = await _requestService.ActivateRequestAsync(id);
            if (!activated)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/deactivate")]
        public async Task<ActionResult> DeactivateRequest(int id)
        {
            var deactivated = await _requestService.DeactivateRequestAsync(id);
            if (!deactivated)
                return NotFound();

            return NoContent();
        }
    }
}
