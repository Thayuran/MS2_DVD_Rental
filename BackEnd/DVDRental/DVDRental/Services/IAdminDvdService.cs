using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;

namespace DVDRental.Services
{
    public interface IAdminDvdService
    {
        Task<List<DVDResponseDTO>> GetAllDVDsAsync();
        Task<DVDResponseDTO> GetDVDByIdAsync(string id);
        Task<DVDResponseDTO> AddDVDAsync(DVDRequestDTO dvd);
        Task UpdateDVDAsync(string id,DVDRequestDTO dvd);
        Task DeleteDVDAsync(string id);
    }
}
