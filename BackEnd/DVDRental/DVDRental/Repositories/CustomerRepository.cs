using DVDRental.Entities;
using Microsoft.Data.SqlClient;

namespace DVDRental.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            string lastCusId = await GetLastCusIdAsync();
            string newCusId = GenerateNewCusId(lastCusId);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Customer (Id,CustomerName, Email,Address,AddressId,PhoneNo,JoinedDate, Action)" +
                    "VALUES (@Id,@FullName, @Email,@Address,@AddressId, @PhoneNumber, @JoinedDate, @Action)", connection);

                command.Parameters.AddWithValue("@Id",newCusId);
                command.Parameters.AddWithValue("@FullName", customer.CustomerName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNo);
                command.Parameters.AddWithValue("@JoinedDate", customer.joined_date);
                command.Parameters.AddWithValue("@Action", customer.Action);

                await command.ExecuteNonQueryAsync();
                return customer;
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customer", connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    customers.Add(new Customer
                    {
                        Id = reader.GetString(reader.GetOrdinal("Id")),
                        CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        AddressId = reader.GetInt32(reader.GetOrdinal("AddressId")),
                        PhoneNo = reader.GetInt32(reader.GetOrdinal("PhoneNo")),
                        joined_date = reader.GetDateTime(reader.GetOrdinal("JoinedDate")),
                        Action = reader.GetBoolean(reader.GetOrdinal("Action"))
                    });
                }
            }

            return customers;
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customer WHERE Id = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Customer
                    {
                        Id = reader.GetString(reader.GetOrdinal("Id")),
                        CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        AddressId = reader.GetInt32(reader.GetOrdinal("AddressId")),
                        PhoneNo = reader.GetInt32(reader.GetOrdinal("PhoneNo")),
                        joined_date = reader.GetDateTime(reader.GetOrdinal("JoinedDate")),
                        Action = reader.GetBoolean(reader.GetOrdinal("Action"))
                    };
                }
                return null;
            }
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Customer SET CustomerName = @FullName, Email = @Email, Address = @Address, " +
                    "AddressId = @AddressId, PhoneNo = @PhoneNumber, JoinedDate = @JoinedDate, Action = @Action " +
                    "WHERE Id = @CustomerId", connection);

                command.Parameters.AddWithValue("@FullName", customer.CustomerName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNo);
                command.Parameters.AddWithValue("@JoinedDate", customer.joined_date);
                command.Parameters.AddWithValue("@Action", customer.Action);
                command.Parameters.AddWithValue("@CustomerId", customer.Id);

                await command.ExecuteNonQueryAsync();
                return customer;
            }
        }

        public async Task<bool> DeleteCustomer(string customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Customer WHERE Id = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> ActivateCustomer(string id)
        {
            return await UpdateCustomerAction(id, true);
        }

        public async Task<bool> DeactivateCustomer(string customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Customer SET Action = 0 WHERE Id = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }


        public async Task<bool> UpdateCustomerAction(string id, bool action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Customers SET Action = @Action WHERE CustomerId = @CustomerId", connection);
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@CustomerId", id);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }



        public async Task<string> GetLastCusIdAsync()
        {
            var sql = "SELECT TOP 1 * FROM Customer ORDER BY Id DESC";

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

        private string GenerateNewCusId(string lastCusId)
        {
            if (string.IsNullOrEmpty(lastCusId))
            {
                return "Cus001";
            }

            string numericPart = lastCusId.Substring(3);
            int numericId = int.Parse(numericPart) + 1;

            return $"Cus{numericId.ToString("D3")}";
        }

    }
}

