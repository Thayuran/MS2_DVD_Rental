using DVDRental.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace DVDRental.Repositories
{
    public class AdminDvdRepository : IAdminDvdRepository
    {
        private readonly string _connectionString;
        private readonly IAdminCategoriesRepository _categoriesRepository;

        public AdminDvdRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public AdminDvdRepository(string connectionString, IAdminCategoriesRepository categoriesRepository)
        {
            _connectionString = connectionString;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<MovieDvd>> GetAllDVDs()
        {
            var movieDvds = new List<MovieDvd>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT DVDs.*, Categories.Name FROM DVDs
                             LEFT JOIN DVD_Categories ON DVDs.ID = DVD_Categories.DVDId
                             LEFT JOIN Categories ON DVD_Categories.CategoryId = Categories.CategoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    Dictionary<int, MovieDvd> movieDict = new Dictionary<int, MovieDvd>();

                    while (await reader.ReadAsync())
                    {
                        int movieId = int.Parse(reader.GetString(0));
                        if (!movieDict.ContainsKey(movieId))
                        {
                            var movieDvd = new MovieDvd
                            {
                                ID = movieId.ToString(),
                                Title = reader.GetString(1),
                                ReleaseDate = reader.GetDateTime(4),
                                Director = reader.GetString(3),
                                Copies = reader.GetInt32(5),
                                Categories= new List<Categories>(),
                            };
                            movieDict.Add(movieId, movieDvd);
                        }

                        if (!reader.IsDBNull(6))
                        {
                            movieDict[movieId].Categories.Add(new Categories
                            {
                                CategoryName = reader.GetString(6)
                            });
                        }
                    }

                    movieDvds = movieDict.Values.ToList();
                }
            }

            return movieDvds;
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
        
        
        public async Task<MovieDvd> AddDVD(MovieDvd movieDvd)

        {
            string lastDvdId = await GetLastDvdIdAsync();
            string newDvdId = GenerateNewDvdId(lastDvdId);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                string query = "INSERT INTO DVDs (Id,Title, Director, ReleaseDate, AvailableCopies, ImagePath) VALUES (@Id,@Title, @Director, @ReleaseDate, @Copies, @ImagePath)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", newDvdId);
                cmd.Parameters.AddWithValue("@Title", movieDvd.Title);
                cmd.Parameters.AddWithValue("@Director", movieDvd.Director);
                cmd.Parameters.AddWithValue("@ReleaseDate", movieDvd.ReleaseDate);
                cmd.Parameters.AddWithValue("@Copies", movieDvd.Copies);
                cmd.Parameters.AddWithValue("@ImagePath", movieDvd.ImagePath ?? (object)DBNull.Value);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();


                foreach (var categoryName in movieDvd.Categories.Select(c => c.CategoryName))
                {

                    var category = await _categoriesRepository.GetByNameAsync(categoryName);
                    if (category == null)
                    {
                        category = new Categories { CategoryName = categoryName };
                        await _categoriesRepository.AddAsync(category);
                    }


                    string categoryLinkQuery = "INSERT INTO DVD_Categories (DVDId, CategoryId) VALUES (@DVDId, @CategoryId)";
                    SqlCommand categoryCmd = new SqlCommand(categoryLinkQuery, conn);
                    categoryCmd.Parameters.AddWithValue("@DVDId", movieDvd.ID);
                    categoryCmd.Parameters.AddWithValue("@CategoryId", category.CategoryID);
                    await categoryCmd.ExecuteNonQueryAsync();
                }
                string selectQuery = "SELECT Id, Title, Director, ReleaseDate, AvailableCopies, ImagePath FROM DVDs WHERE Id = @Id";
                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@Id", movieDvd.ID);

                using (var reader = await selectCmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        movieDvd.ID = reader.GetString(0);
                        movieDvd.Title = reader.GetString(1);
                        movieDvd.Director = reader.GetString(2);
                        movieDvd.ReleaseDate = reader.GetDateTime(3);
                        movieDvd.Copies = reader.GetInt32(4);
                        movieDvd.ImagePath = reader.IsDBNull(5) ? null : reader.GetString(5);
                    }
                }
            }
            return movieDvd;
        }
    
        public async Task<MovieDvd> GetMovieById(string id)
        {
            MovieDvd movieDvd = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            SELECT 
                d.Id, d.Title, d.Director, d.ReleaseDate, d.AvailableCopies, d.ImagePath, 
                c.CategoryId, c.Name AS CategoryName
            FROM 
                DVDs d
            LEFT JOIN 
                DVD_Categories dc ON d.Id = dc.DVDId
            LEFT JOIN 
                Categories c ON dc.CategoryId = c.CategoryId
            WHERE 
                d.Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    if (movieDvd == null)
                    {
                        movieDvd = new MovieDvd
                        {
                            ID = reader.GetString(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Director = reader.GetString(reader.GetOrdinal("Director")),
                            ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                            Copies = reader.GetInt32(reader.GetOrdinal("AvailableCopies")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                            Categories = new List<Categories>()
                        };
                    }


                    if (!reader.IsDBNull(reader.GetOrdinal("CategoryId")))
                    {
                        var category = new Categories
                        {
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                        };
                        movieDvd.Categories.Add(category);
                    }
                }

                reader.Close();
            }

            return movieDvd;
        }


        public async Task UpdateAsync(MovieDvd movieDvd)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Update DVD details
                string query = @"UPDATE DVDs 
                         SET Title = @Title, Director = @Director, ReleaseDate = @ReleaseDate, AvailableCopies = @Copies, ImagePath = @ImagePath
                         WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", movieDvd.ID);
                cmd.Parameters.AddWithValue("@Title", movieDvd.Title);
                cmd.Parameters.AddWithValue("@Director", movieDvd.Director);
                cmd.Parameters.AddWithValue("@ReleaseDate", movieDvd.ReleaseDate);
                cmd.Parameters.AddWithValue("@Copies", movieDvd.Copies);
                cmd.Parameters.AddWithValue("@ImagePath", movieDvd.ImagePath ?? (object)DBNull.Value);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();

                string deleteQuery = @"DELETE FROM DVD_Categories 
                               WHERE DVDId = @DVDId AND CategoryId NOT IN (
                                   SELECT CategoryId FROM Categories WHERE Name IN @CategoryNames
                               )";

                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@DVDId", movieDvd.ID);
                deleteCmd.Parameters.AddWithValue("@CategoryNames", string.Join(",", movieDvd.Categories.Select(c => c.CategoryName)));
                await deleteCmd.ExecuteNonQueryAsync();


                foreach (var category in movieDvd.Categories)
                {
                    var existingCategory = await _categoriesRepository.GetByNameAsync(category.CategoryName);
                    if (existingCategory == null)
                    {

                        existingCategory = new Categories { CategoryName = category.CategoryName };
                        await _categoriesRepository.AddAsync(existingCategory);
                    }


                    string linkQuery = @"IF NOT EXISTS (SELECT 1 FROM DVD_Categories WHERE DVDId = @DVDId AND CategoryId = @CategoryId)
                                 INSERT INTO DVD_Categories (DVDId, CategoryId) VALUES (@DVDId, @CategoryId)";

                    SqlCommand linkCmd = new SqlCommand(linkQuery, conn);
                    linkCmd.Parameters.AddWithValue("@DVDId", movieDvd.ID);
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


        public async Task<string> GetLastDvdIdAsync()
        {
            var sql = "SELECT TOP 1 * FROM DVDs ORDER BY Id DESC";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        private string GenerateNewDvdId(string lastDvdId)
        {
            if (string.IsNullOrEmpty(lastDvdId))
            {
                return "dvd001";
            }

            string numericPart = lastDvdId.Substring(3);
            int numericId = int.Parse(numericPart) + 1;

            return $"dvd{numericId.ToString("D3")}";
        }
    }
}
