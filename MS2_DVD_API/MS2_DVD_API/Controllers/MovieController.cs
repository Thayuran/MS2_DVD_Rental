using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<ActionResult<List<MovieReponse>>> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReponse>> GetMovieById(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);
                return Ok(movie);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovieReponse>> AddMovie([FromBody] MovieRequest movieRequest)
        {
            if (movieRequest == null)
            {
                return BadRequest("Movie data is required.");
            }

            var addedMovie = await _movieService.AddMovieAsync(movieRequest);
            return CreatedAtAction(nameof(GetMovieById), new { id = addedMovie.MovieID }, addedMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieRequest movieRequest)
        {
            if (movieRequest == null)
            {
                return BadRequest("Movie data is required.");
            }

            try
            {
                await _movieService.UpdateMovieAsync(id, movieRequest);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
