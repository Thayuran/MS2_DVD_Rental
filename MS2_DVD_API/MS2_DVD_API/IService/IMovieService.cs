using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.IService
{
    public interface IMovieService
    {
        Task<List<MovieReponse>> GetAllMoviesAsync();
        Task<MovieReponse> GetMovieByIdAsync(int id);
        Task<MovieReponse> AddMovieAsync(MovieRequest movieRequest);
        Task UpdateMovieAsync(int id, MovieRequest movieRequest);
        Task DeleteMovieAsync(int id);
    }
}
