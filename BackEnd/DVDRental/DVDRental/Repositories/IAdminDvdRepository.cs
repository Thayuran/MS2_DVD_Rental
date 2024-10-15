using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface IAdminDvdRepository
    {
        public Task<List<MovieDvd>> GetAllDVDs();
        public string GenerateDVDID();
        public Task AddDVD(MovieDvd movieDvd);
        public MovieDvd GetMovieById(string id);


    }
}
