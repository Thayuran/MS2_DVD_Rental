using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;
using DVDRental.Repositories;

namespace DVDRental.Services
{
    public class AdminCategoriesService:IAdminCategoriesService
    {
        private readonly IAdminCategoriesRepository _categoryRepository;

        public AdminCategoriesService(IAdminCategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Get all categories
        public async Task<List<CategoryResponseDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(category => new CategoryResponseDTO
            {
                CategoryId = category.CategoryID,
                Name = category.CategoryName,
            }).ToList();
        }

        // Get category by ID
        public async Task<CategoryResponseDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return null;

            return new CategoryResponseDTO
            {
                CategoryId = category.CategoryID,
                Name = category.CategoryName
            };
        }

        // Add a new category
        public async Task<CategoryResponseDTO> AddAsync(CategoryRequestDTO categoryRequest)
        {
            var category = new Categories
            {
                CategoryName = categoryRequest.Name
            };

            var data=await _categoryRepository.AddAsync(category);

            var res = new CategoryResponseDTO { 
                CategoryId=data.CategoryID,
                Name = data.CategoryName
            };
            return res;

        }

        /*// Update an existing category
        public async Task UpdateAsync(int id, CategoryRequestDTO categoryRequest)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return;

            category.CategoryName = categoryRequest.Name;

            await _categoryRepository.UpdateAsync(category);
        }

        // Delete a category
        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }*/
    }
}
