using DVDRental.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace DVDRental.Repositories
{
    public class AdminDvdRepository : IAdminDvdRepository
    {
        private readonly string _connectionString;

        public AdminDvdRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<List<MovieDvd>> GetAllDVDs()
        {
            var movielist=new List<MovieDvd>();
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                var cmd=connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM DVDs";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movielist.Add(new MovieDvd
                        {
                            ID = reader.GetString(0),
                            MovieName= reader.GetString(1),
                            Director=reader.GetString(2),
                            Categories=reader.GetString(3),
                            ReleaseDate=reader.GetDateTime(4),
                            Copies=reader.GetInt32(5)
                        });


                    }
                }
                return movielist ;
            }
        }


        public string GenerateDVDID()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(ID) FROM DVDs";
                using (var command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        string currentMaxID = result.ToString();
                        int numericPart = int.Parse(currentMaxID.Replace("DVD_", ""));
                        numericPart++;
                        return $"DVD_{numericPart:D3}";
                    }
                    else
                    {
                        return "DVD_001";
                    }
                }
            }
        }
        public async Task AddDVD(MovieDvd movieDvd)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command=connection.CreateCommand();
                movieDvd.ID = GenerateDVDID();
                command.CommandText= "INSERT INTO DVDs(ID,Title,Director, Genre, ReleaseDate, Copies)" +
                    "VALUES(@id,@Title, @Director, @Genre, @ReleaseDate, @CopiesAvailable)";
                command.Parameters.AddWithValue("@id",movieDvd.ID);
                command.Parameters.AddWithValue("@Title", movieDvd.MovieName);
                command.Parameters.AddWithValue("@Director",movieDvd.Director);
                command.Parameters.AddWithValue("@Genre",movieDvd.Categories);
                command.Parameters.AddWithValue("@ReleaseDate", movieDvd.ReleaseDate);
                command.Parameters.AddWithValue("@CopiesAvailable", movieDvd.Copies);
                command.ExecuteNonQuery();
                
            };

        }
    
        public MovieDvd GetMovieById(string id)
        {
            
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM DVDs WHERE ID=@dvdid";
                cmd.Parameters.AddWithValue ("@dvdid", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new MovieDvd
                        {
                            ID = reader.GetString(0),
                            MovieName = reader.GetString(1),
                            Director = reader.GetString(2),
                            Categories = reader.GetString(3),
                            ReleaseDate = reader.GetDateTime(4),
                            Copies = reader.GetInt16(5)
                        };


                    }
                }
                return null;
            }
        }
    
    }
}
