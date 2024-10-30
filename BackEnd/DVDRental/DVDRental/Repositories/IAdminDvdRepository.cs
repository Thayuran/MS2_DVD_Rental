using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface IAdminDvdRepository
    {
        Task<List<MovieDvd>> GetAllDVDs();

        Task<MovieDvd> AddDVD(MovieDvd movieDvd);
        Task<MovieDvd> GetMovieById(string id);

        Task UpdateAsync(MovieDvd dvd);
        Task DeleteAsync(String id);


        Task<string> GetLastDvdIdAsync();

        Task<List<MovieDvd>> GetDVDsByCategoryAsync(int categoryId);
        Task AddDVDToCategoryAsync(string dvdId, int categoryId);
        Task RemoveDVDFromCategoryAsync(string dvdId, int categoryId);

    }
}
