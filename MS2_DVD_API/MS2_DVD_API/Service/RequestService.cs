using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;

namespace MS2_DVD_API.Service
{
    public class RequestService:IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
    }
}
