using MS2_DVD_API.Entity;

namespace MS2_DVD_API.IRepository
{
    public interface ImovieRepository
    {
        Task<List<Movies>> GetAllMovies();
        Task<Movies> GetMovieById(int movieId);
        Task<Movies> AddMovie(Movies movie);
        Task<Movies> UpdateMovie(Movies movie);
        Task<bool> DeleteMovie(int movieId);
    }
        
}
