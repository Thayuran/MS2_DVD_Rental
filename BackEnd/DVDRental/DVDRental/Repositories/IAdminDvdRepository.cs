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

    }
}
