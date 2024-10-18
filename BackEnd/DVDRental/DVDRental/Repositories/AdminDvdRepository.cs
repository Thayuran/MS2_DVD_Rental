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


        public async Task UpdateAsync(MovieDvd entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Update DVD details
                string query = @"UPDATE DVDs 
                         SET Title = @Title, Director = @Director, ReleaseDate = @ReleaseDate, AvailableCopies = @Copies, ImagePath = @ImagePath
                         WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", entity.ID);
                cmd.Parameters.AddWithValue("@Title", entity.Title);
                cmd.Parameters.AddWithValue("@Director", entity.Director);
                cmd.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate);
                cmd.Parameters.AddWithValue("@Copies", entity.Copies);
                cmd.Parameters.AddWithValue("@ImagePath", entity.ImagePath ?? (object)DBNull.Value);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();

                string deleteQuery = @"DELETE FROM DVD_Categories 
                               WHERE DVDId = @DVDId AND CategoryId NOT IN (
                                   SELECT CategoryId FROM Categories WHERE Name IN @CategoryNames
                               )";

                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@DVDId", entity.ID);
                deleteCmd.Parameters.AddWithValue("@CategoryNames", string.Join(",", entity.Categories.Select(c => c.CategoryName)));
                await deleteCmd.ExecuteNonQueryAsync();


                foreach (var category in entity.Categories)
                {
                    var existingCategory = await _categoryRepository.GetByNameAsync(category.CategoryName);
                    if (existingCategory == null)
                    {

                        existingCategory = new Categories { CategoryName = category.CategoryName };
                        await _categoryRepository.AddAsync(existingCategory);
                    }


                    string linkQuery = @"IF NOT EXISTS (SELECT 1 FROM DVD_Categories WHERE DVDId = @DVDId AND CategoryId = @CategoryId)
                                 INSERT INTO DVD_Categories (DVDId, CategoryId) VALUES (@DVDId, @CategoryId)";

                    SqlCommand linkCmd = new SqlCommand(linkQuery, conn);
                    linkCmd.Parameters.AddWithValue("@DVDId", entity.ID);
                    linkCmd.Parameters.AddWithValue("@CategoryId", existingCategory.CategoryID);
                    await linkCmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(string dvdId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();


                string deleteCategoryLinkQuery = "DELETE FROM DVD_Categories WHERE DVDId = @DVDId";
                SqlCommand deleteCategoryLinkCmd = new SqlCommand(deleteCategoryLinkQuery, conn);
                deleteCategoryLinkCmd.Parameters.AddWithValue("@DVDId", dvdId);
                await deleteCategoryLinkCmd.ExecuteNonQueryAsync();


                string deleteDvdQuery = "DELETE FROM DVDs WHERE Id = @DVDId";
                SqlCommand deleteDvdCmd = new SqlCommand(deleteDvdQuery, conn);
                deleteDvdCmd.Parameters.AddWithValue("@DVDId", dvdId);
                await deleteDvdCmd.ExecuteNonQueryAsync();
            }
        }

    }
}
