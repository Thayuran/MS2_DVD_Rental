using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;

namespace DVDRental.Services
{
    public interface IAdminCategoriesService
    {
        Task<List<CategoryResponseDTO>> GetAllAsync();
        Task<CategoryResponseDTO> GetByIdAsync(int id);
        Task<CategoryResponseDTO> AddAsync(CategoryRequestDTO category);

       /* Task UpdateAsync(int id,CategoryRequestDTO category);
        Task DeleteAsync(int id);*/
    }
}
