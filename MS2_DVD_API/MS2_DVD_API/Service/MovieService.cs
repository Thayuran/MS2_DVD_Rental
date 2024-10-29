using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MS2_DVD_API.Entity;
using System;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Modals.RequestModal;
using MS2_DVD_API.Modals.ResponseModal;
using MS2_DVD_API.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS2_DVD_API.Service
{
    public class MovieService : IMovieService
    {
        private readonly ImovieRepository _movieRepository;

        public MovieService(ImovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieReponse>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMovies();
            return movies.Select(movie => new MovieReponse
            {
                MovieID = movie.movieID,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                ReleaseDate = movie.ReleaseDate,
                Cast = movie.Cast,
                NoOfCopies = movie.NoOfCopies
            }).ToList();
        }

        public async Task<MovieReponse> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with ID {id} not found.");

            return new MovieReponse
            {
                MovieID = movie.movieID,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                ReleaseDate = movie.ReleaseDate,
                Cast = movie.Cast,
                NoOfCopies = movie.NoOfCopies
            };
        }

        public async Task<MovieReponse> AddMovieAsync(MovieRequest movieRequest)
        {
            var movie = new Movies
            {
                Title = movieRequest.Title,
                Genre = movieRequest.Genre,
                Director = movieRequest.Director,
                ReleaseDate = movieRequest.ReleaseDate,
                Cast = movieRequest.Cast,
                NoOfCopies = movieRequest.NoOfCopies
            };

            var result = await _movieRepository.AddMovie(movie);
            return new MovieReponse
            {
                MovieID = result.movieID,
                Title = result.Title,
                Genre = result.Genre,
                Director = result.Director,
                ReleaseDate = result.ReleaseDate,
                Cast = result.Cast,
                NoOfCopies = result.NoOfCopies
            };
        }

        public async Task UpdateMovieAsync(int id, MovieRequest movieRequest)
        {
            var existingMovie = await _movieRepository.GetMovieById(id);
            if (existingMovie == null)
                throw new KeyNotFoundException($"Movie with ID {id} not found.");

            existingMovie.Title = movieRequest.Title;
            existingMovie.Genre = movieRequest.Genre;
            existingMovie.Director = movieRequest.Director;
            existingMovie.ReleaseDate = movieRequest.ReleaseDate;
            existingMovie.Cast = movieRequest.Cast;
            existingMovie.NoOfCopies = movieRequest.NoOfCopies;

            await _movieRepository.UpdateMovie(existingMovie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with ID {id} not found.");

            await _movieRepository.DeleteMovie(id);
        }

    }
}







