using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ImovieRepository _movieRepository;

        public MovieController(ImovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie(Movies movie)
        {
            var createdMovie = await _movieRepository.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.movieID }, createdMovie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movies movie)
        {
            if (id != movie.movieID)
            {
                return BadRequest("Movie ID mismatch");
            }

            var updatedMovie = await _movieRepository.UpdateMovie(movie);
            if (updatedMovie == null)
            {
                return NotFound();
            }

            return Ok(updatedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var isDeleted = await _movieRepository.DeleteMovie(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies(string title, string genre, string director, string cast)
        {
            var movies = await _movieRepository.SearchMovies(title, genre, director, cast);
            return Ok(movies);
        }

        [HttpPut("update-copies/{id}")]
        public async Task<IActionResult> UpdateMovieCopies(int id, int noOfCopies)
        {
            var isUpdated = await _movieRepository.UpdateMovieCopies(id, noOfCopies);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok(new { message = "Number of copies updated successfully." });
        }
    }
}
