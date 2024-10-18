using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface IAdminCategoriesRepository
    {
        Task<List<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(int categoryId);
        Task<Categories> AddAsync(Categories category);
        Task<Categories> GetByNameAsync(string name);
    }
}
