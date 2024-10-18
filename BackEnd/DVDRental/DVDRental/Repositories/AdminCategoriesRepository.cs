using DVDRental.Entities;
using Microsoft.Data.SqlClient;

namespace DVDRental.Repositories
{
    public class AdminCategoriesRepository:IAdminCategoriesRepository
    {
        private readonly string _connectionString;

        public AdminCategoriesRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            var categories = new List<Categories>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categories.Add(new Categories
                        {
                            CategoryID = reader.GetInt32(0),
                            CategoryName = reader.GetString(1)
                        });
                    }
                }
            }

            return categories;
        }

        public async Task<Categories> GetByIdAsync(int categoryId)
        {
            Categories category = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                conn.Open();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        category = new Categories
                        {
                            CategoryID = reader.GetInt32(0),
                            CategoryName = reader.GetString(1)
                        };
                    }
                }
            }

            return category;
        }

        public async Task<Categories> AddAsync(Categories category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Categories (Name) VALUES (@Name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.CategoryName);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            return category;
        }

        public async Task<Categories> GetByNameAsync(string name)
        {
            Categories category = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories WHERE Name = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);

                conn.Open();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        category = new Categories
                        {
                            CategoryID = reader.GetInt32(0),
                            CategoryName = reader.GetString(1)
                        };
                    }
                }
            }

            return category;
        }
    }
}
