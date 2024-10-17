using MS2_DVD_API.Entity;

namespace MS2_DVD_API.IRepository
{
    public interface ImovieRepository
    {
        Task<Movies> AddMovie(Movies movie);
        Task<List<Movies>> GetAllMovies();
        Task<Movies> GetMovieById(int movieId);
        Task<Movies> UpdateMovie(Movies movie);
        Task<bool> DeleteMovie(int movieId);
        Task<List<Movies>> SearchMovies(string title, string genre, string director, string cast);
        Task<bool> UpdateMovieCopies(int movieId, int noOfCopies);
    }
}
