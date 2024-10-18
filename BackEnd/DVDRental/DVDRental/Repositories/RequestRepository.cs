using DVDRental.Entities;
using Microsoft.Data.SqlClient;

namespace DVDRental.Repositories
{
    public class RequestRepository:IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Rent_Request>> GetAllRequestsAsync()
        {
            var requests = new List<Rent_Request>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Requests";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var request = new Rent_Request
                        {
                            ID = reader["ID"].ToString(),
                            customerId = reader["customerId"].ToString(),
                            movieId = reader["movieId"].ToString(),
                            Status = reader["Status"].ToString(),
                            RequestDate = Convert.ToDateTime(reader["RequestDate"])
                        };

                        requests.Add(request);
                    }
                }
            }

            return requests;
        }
        public async Task<Rent_Request> GetRequestByIdAsync(string id)
        {
            Rent_Request request = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Requests WHERE ID = @ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            request = new Rent_Request
                            {
                                ID = reader["ID"].ToString(),
                                customerId = reader["customerId"].ToString(),
                                movieId = reader["movieId"].ToString(),
                                Status = reader["Status"].ToString(),
                                RequestDate = Convert.ToDateTime(reader["RequestDate"])
                            };
                        }
                    }
                }
            }

            return request;
        }
       
        
        public async Task<Rent_Request> AddRequestAsync(Rent_Request request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO Requests (ID, customerId, movieId, Status, RequestDate) 
                             VALUES (@ID, @customerId, @movieId, @Status, @RequestDate)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@customerId", request.customerId);
                    command.Parameters.AddWithValue("@movieId", request.movieId);
                    command.Parameters.AddWithValue("@Status", request.Status);
                    command.Parameters.AddWithValue("@RequestDate", request.RequestDate);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return request;
        }
        public async Task UpdateRequestAsync(Rent_Request request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE Requests SET customerId = @customerId, movieId = @movieId, 
                             Status = @Status, RequestDate = @RequestDate WHERE ID = @ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@customerId", request.customerId);
                    command.Parameters.AddWithValue("@movieId", request.movieId);
                    command.Parameters.AddWithValue("@Status", request.Status);
                    command.Parameters.AddWithValue("@RequestDate", request.RequestDate);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteRequestAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Requests WHERE ID = @ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    
    }
}
