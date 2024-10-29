using Microsoft.Data.SqlClient;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Repository
{
    public class RequestRepository:IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<RequestRespone> AddRequest(RequestRespone request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Request (CustomerId, MovieId, RequestDate, Action) " +
                    "VALUES (@CustomerId, @MovieId, @RequestDate, @Action); " +
                    "SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                command.Parameters.AddWithValue("@MovieId", request.MovieID);
                command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                command.Parameters.AddWithValue("@Action", request.Action);

                request.Id = Convert.ToInt32(await command.ExecuteScalarAsync());

                return request;
            }
        }

        public async Task<List<RequestRespone>> GetAllRequests()
        {
            var requests = new List<RequestRespone>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Request", connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    requests.Add(new RequestRespone
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("RequestId")),
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        MovieID = reader.GetInt32(reader.GetOrdinal("MovieId")),
                        RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                        Action = reader.GetBoolean(reader.GetOrdinal("Action"))
                    });
                }
            }

            return requests;
        }

        public async Task<RequestRespone> GetRequestById(int requestId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Request WHERE RequestId = @RequestId", connection);
                command.Parameters.AddWithValue("@RequestId", requestId);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new RequestRespone
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("RequestId")),
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        MovieID = reader.GetInt32(reader.GetOrdinal("MovieId")),
                        RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                        Action = reader.GetBoolean(reader.GetOrdinal("Action"))
                    };
                }
                return null;
            }
        }

        public async Task<RequestRespone> UpdateRequest(RequestRespone request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Request SET CustomerId = @CustomerId, MovieId = @MovieId, RequestDate = @RequestDate, Action = @Action " +
                    "WHERE RequestId = @RequestId", connection);

                command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                command.Parameters.AddWithValue("@MovieId", request.MovieID);
                command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                command.Parameters.AddWithValue("@Action", request.Action);
                command.Parameters.AddWithValue("@RequestId", request.Id);

                await command.ExecuteNonQueryAsync();

                return request;
            }
        }

        public async Task<bool> DeleteRequest(int requestId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Request WHERE RequestId = @RequestId", connection);
                command.Parameters.AddWithValue("@RequestId", requestId);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> ActivateRequest(int requestId)
        {
            return await UpdateRequestAction(requestId, true);
        }

        public async Task<bool> DeactivateRequest(int requestId)
        {
            return await UpdateRequestAction(requestId, false);
        }

        private async Task<bool> UpdateRequestAction(int requestId, bool action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Request SET Action = @Action WHERE RequestId = @RequestId", connection);
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@RequestId", requestId);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

    }
}
