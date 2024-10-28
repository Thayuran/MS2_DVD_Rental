using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.Modals.RequestModal;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;

        public RequestController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        
    }
}
