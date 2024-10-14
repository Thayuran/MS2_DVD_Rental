using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Customer> GetAll() 
        { 
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText= "SELECT CustomerId,FirstName,LastName,Address"
            }
        }
    }
}
