using Microsoft.Data.SqlClient;

namespace MS2_DVD_API.Data
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customer')
                BEGIN
                    CREATE TABLE Customer (
                        CustomerId INT PRIMARY KEY IDENTITY(1,1), 
                        FullName NVARCHAR(50) NOT NULL,
                        Email NVARCHAR(100) NOT NULL,
                        Address NVARCHAR(100) NOT NULL,
                        AddressId INT NOT NULL,
                        PhoneNumber NVARCHAR(15) NOT NULL, 
                        JoinedDate DATETIME2 NOT NULL,
                        Action BIT NOT NULL
                    );
                END;
            ";
                command.ExecuteNonQuery();
            }
        }
    }
}
