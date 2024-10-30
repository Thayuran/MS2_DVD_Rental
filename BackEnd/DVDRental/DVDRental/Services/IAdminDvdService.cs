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

        Task<string> GenerateNewDvdIdAsync();

       /* Task<List<DVDResponseDTO>> GetDVDsByCategoryAsync(int categoryId);
        Task AddDVDToCategoryAsync(string dvdId, int categoryId);
        Task RemoveDVDFromCategoryAsync(string dvdId, int categoryId);*/
    }
}
