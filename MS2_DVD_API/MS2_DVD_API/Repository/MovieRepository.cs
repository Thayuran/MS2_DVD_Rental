using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Repository
{
    public class MovieRepository:ImovieRepository
    {

        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Movies> AddMovie(Movies movie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Movies (Title, Genre, Director, ReleaseDate, Cast, NoOfCopies) " +
                    "VALUES (@Title, @Genre, @Director, @ReleaseDate, @Cast, @NoOfCopies)", connection);

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Genre", movie.Genre);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
                command.Parameters.AddWithValue("@Cast", movie.Cast);
                command.Parameters.AddWithValue("@NoOfCopies", movie.NoOfCopies);

                await command.ExecuteNonQueryAsync();
                return movie;
            }
        }

        public async Task<List<Movies>> GetAllMovies()
        {
            var movies = new List<Movies>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Movies", connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    movies.Add(new Movies
                    {
                        movieID = reader.GetInt32(reader.GetOrdinal("MovieID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Genre = reader.GetString(reader.GetOrdinal("Genre")),
                        Director = reader.GetString(reader.GetOrdinal("Director")),
                        ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        Cast = reader.GetString(reader.GetOrdinal("Cast")),
                        NoOfCopies = reader.GetInt32(reader.GetOrdinal("NoOfCopies"))
                    });
                }
            }

            return movies;
        }

        public async Task<Movies> GetMovieById(int movieId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Movies WHERE MovieID = @MovieID", connection);
                command.Parameters.AddWithValue("@MovieID", movieId);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Movies
                    {
                        movieID = reader.GetInt32(reader.GetOrdinal("MovieID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Genre = reader.GetString(reader.GetOrdinal("Genre")),
                        Director = reader.GetString(reader.GetOrdinal("Director")),
                        ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        Cast = reader.GetString(reader.GetOrdinal("Cast")),
                        NoOfCopies = reader.GetInt32(reader.GetOrdinal("NoOfCopies"))
                    };
                }
                return null;
            }
        }

        public async Task<Movies> UpdateMovie(Movies movie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Movies SET Title = @Title, Genre = @Genre, Director = @Director, " +
                    "ReleaseDate = @ReleaseDate, Cast = @Cast, NoOfCopies = @NoOfCopies " +
                    "WHERE MovieID = @MovieID", connection);

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Genre", movie.Genre);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
                command.Parameters.AddWithValue("@Cast", movie.Cast);
                command.Parameters.AddWithValue("@NoOfCopies", movie.NoOfCopies);
                command.Parameters.AddWithValue("@MovieID", movie.movieID);

                await command.ExecuteNonQueryAsync();
                return movie;
            }
        }

        public async Task<bool> DeleteMovie(int movieId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Movies WHERE MovieID = @MovieID", connection);
                command.Parameters.AddWithValue("@MovieID", movieId);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<List<Movies>> SearchMovies(string title, string genre, string director, string cast)
        {
            var movies = new List<Movies>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "SELECT * FROM Movies WHERE " +
                    "(Title LIKE @Title OR @Title IS NULL) AND " +
                    "(Genre LIKE @Genre OR @Genre IS NULL) AND " +
                    "(Director LIKE @Director OR @Director IS NULL) AND " +
                    "(Cast LIKE @Cast OR @Cast IS NULL)", connection);

                command.Parameters.AddWithValue("@Title", string.IsNullOrEmpty(title) ? (object)DBNull.Value : $"%{title}%");
                command.Parameters.AddWithValue("@Genre", string.IsNullOrEmpty(genre) ? (object)DBNull.Value : $"%{genre}%");
                command.Parameters.AddWithValue("@Director", string.IsNullOrEmpty(director) ? (object)DBNull.Value : $"%{director}%");
                command.Parameters.AddWithValue("@Cast", string.IsNullOrEmpty(cast) ? (object)DBNull.Value : $"%{cast}%");

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    movies.Add(new Movies
                    {
                        movieID = reader.GetInt32(reader.GetOrdinal("MovieID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Genre = reader.GetString(reader.GetOrdinal("Genre")),
                        Director = reader.GetString(reader.GetOrdinal("Director")),
                        ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        Cast = reader.GetString(reader.GetOrdinal("Cast")),
                        NoOfCopies = reader.GetInt32(reader.GetOrdinal("NoOfCopies"))
                    });
                }
            }

            return movies;
        }

        public async Task<bool> UpdateMovieCopies(int movieId, int noOfCopies)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Movies SET NoOfCopies = @NoOfCopies WHERE MovieID = @MovieID", connection);

                command.Parameters.AddWithValue("@NoOfCopies", noOfCopies);
                command.Parameters.AddWithValue("@MovieID", movieId);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
