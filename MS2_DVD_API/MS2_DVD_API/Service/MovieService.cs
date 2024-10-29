using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;

namespace MS2_DVD_API.Service
{
    public class MovieService:IMovieService
    {
        private readonly ImovieRepository _movieRepository;

        public MovieService(ImovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
    }
}
