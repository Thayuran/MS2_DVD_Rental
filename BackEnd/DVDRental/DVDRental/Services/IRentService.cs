using DVDRental.DTOs.ResponseDTO;

namespace DVDRental.Services
{
    public interface IRentService
    {

        Task<List<RentalResponseDTO>> GetAllRentalsAsync();
        Task<RentalResponseDTO> GetRentalByIdAsync(string id);
        Task<RentalResponseDTO> CreateRentalFromRequestAsync(string requestId);
        Task ReturnDVDAsync(string rentalId, bool isDamaged);
        Task<List<RentalResponseDTO>> GetOverdueRentalsAsync();
        Task CalculateAndApplyFinesAsync();
    }
}
