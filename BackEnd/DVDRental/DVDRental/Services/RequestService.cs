using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;

namespace DVDRental.Services
{
    public class RequestService:IRequestService
    {
        private readonly List<RequestResponseDTO> _requests;

        public RequestService()
        {
            _requests = new List<RequestResponseDTO>();
        }

        public async Task<List<RequestResponseDTO>> GetAllRequestsAsync()
        {
            await Task.Delay(100);
            return _requests;
        }

        public async Task<RequestResponseDTO> GetRequestByIdAsync(string id)
        {
            await Task.Delay(50);
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public async Task<RequestResponseDTO> AddRequestAsync(RequestDTO requestDto)
        {
            // Simulating async operation
            await Task.Delay(100);

            var newRequest = new RequestResponseDTO
            {
                Id = Guid.NewGuid().ToString(),
                Status = "Pending",
                
            };

            _requests.Add(newRequest);
            return newRequest;
        }

        public async Task UpdateRequestStatusAsync(string id, string status)
        {
            // Simulating async operation
            await Task.Delay(50);

            var request = _requests.FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                request.Status = status;
            }
        }
    }
}
