using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface IRentRepository
    {
        Task<List<Movie_Rent>> GetAllRentalsAsync();
        Task<Movie_Rent> GetRentalByIdAsync(string id);
        Task<Movie_Rent> AddRentalAsync(Movie_Rent rental);
        Task UpdateRentalAsync(Movie_Rent rental);
        Task<List<Movie_Rent>> GetOverdueRentalsAsync();
    }
}
