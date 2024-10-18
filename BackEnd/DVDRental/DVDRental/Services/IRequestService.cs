using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;

namespace DVDRental.Services
{
    public interface IRequestService
    {
        Task<List<RequestResponseDTO>> GetAllRequestsAsync();
        Task<RequestResponseDTO> GetRequestByIdAsync(string id);
        Task<RequestResponseDTO> AddRequestAsync(RequestDTO requestDto);
        Task UpdateRequestStatusAsync(string id, string status);
    }
}
