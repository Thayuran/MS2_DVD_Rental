using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Service
{
    public class RequestService:IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<List<RequestRespone>> GetAllRequestsAsync()
        {
            var requests = await _requestRepository.GetAllRequests();
            return requests.Select(request => new RequestRespone
            {
                Id = request.Id,
                CustomerId = request.CustomerId,
                MovieID = request.MovieID,
                RequestDate = request.RequestDate,
                Action = request.Action
            }).ToList();
        }

        public async Task<RequestRespone> GetRequestByIdAsync(int id)
        {
            var request = await _requestRepository.GetRequestById(id);
            if (request == null)
                throw new KeyNotFoundException($"Request with ID {id} not found.");

            return new RequestRespone
            {
                Id = request.Id,
                CustomerId = request.CustomerId,
                MovieID = request.MovieID,
                RequestDate = request.RequestDate,
                Action = request.Action
            };
        }

        public async Task<RequestRespone> AddRequestAsync(RequestRespone requestRequest)
        {
            // Adjust the mapping if necessary
            var request = new RequestRespone
            {
                CustomerId = requestRequest.CustomerId,
                MovieID = requestRequest.MovieID,
                RequestDate = requestRequest.RequestDate,
                Action = requestRequest.Action
            };

            var result = await _requestRepository.AddRequest(request);
            return new RequestRespone
            {
                Id = result.Id,
                CustomerId = result.CustomerId,
                MovieID = result.MovieID,
                RequestDate = result.RequestDate,
                Action = result.Action
            };
        }

        public async Task UpdateRequestAsync(int id, RequestRespone requestRequest)
        {
            var existingRequest = await _requestRepository.GetRequestById(id);
            if (existingRequest == null)
                throw new KeyNotFoundException($"Request with ID {id} not found.");

            // Update the properties
            existingRequest.CustomerId = requestRequest.CustomerId;
            existingRequest.MovieID = requestRequest.MovieID;
            existingRequest.RequestDate = requestRequest.RequestDate;
            existingRequest.Action = requestRequest.Action;

            await _requestRepository.UpdateRequest(existingRequest);
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await _requestRepository.GetRequestById(id);
            if (request == null)
                throw new KeyNotFoundException($"Request with ID {id} not found.");

            await _requestRepository.DeleteRequest(id);
        }

        public async Task<bool> ActivateRequestAsync(int requestId)
        {
            return await _requestRepository.ActivateRequest(requestId);
        }

        public async Task<bool> DeactivateRequestAsync(int requestId)
        {
            return await _requestRepository.DeactivateRequest(requestId);
        }
    }
}
