using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.Modals.RequestModal;

namespace MS2_DVD_API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Customers (FullName, Email, Address, AddressId, PhoneNumber, JoinedDate, Action)" +
                    "VALUES (@FullName, @Email, @Address, @AddressId, @PhoneNumber, @JoinedDate, @Action)", connection);

                command.Parameters.AddWithValue("@FullName", customer.FullName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                command.Parameters.AddWithValue("@PhoneNumber", customer.phoneNumber);
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
                var command = new SqlCommand("SELECT * FROM Customers", connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        AddressId = reader.GetInt32(reader.GetOrdinal("AddressId")),
                        phoneNumber = reader.GetInt32(reader.GetOrdinal("PhoneNumber")),
                        joined_date = reader.GetDateTime(reader.GetOrdinal("JoinedDate")),
                        Action = reader.GetBoolean(reader.GetOrdinal("Action"))
                    });
                }
            }

            return customers;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customers WHERE CustomerId = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Customer
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        AddressId = reader.GetInt32(reader.GetOrdinal("AddressId")),
                        phoneNumber = reader.GetInt32(reader.GetOrdinal("PhoneNumber")),
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
                    "UPDATE Customers SET FullName = @FullName, Email = @Email, Address = @Address, " +
                    "AddressId = @AddressId, PhoneNumber = @PhoneNumber, JoinedDate = @JoinedDate, Action = @Action " +
                    "WHERE CustomerId = @CustomerId", connection);

                command.Parameters.AddWithValue("@FullName", customer.FullName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                command.Parameters.AddWithValue("@PhoneNumber", customer.phoneNumber);
                command.Parameters.AddWithValue("@JoinedDate", customer.joined_date);
                command.Parameters.AddWithValue("@Action", customer.Action);
                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

                await command.ExecuteNonQueryAsync();
                return customer;
            }
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> ActivateCustomer(int id)
        {
            return await UpdateCustomerAction(id, true);
        }

        public async Task<bool> DeactivateCustomer(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Customers SET Action = 0 WHERE CustomerId = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }


        public async Task<bool> UpdateCustomerAction(int id, bool action)
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
       

    }
}
