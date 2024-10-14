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
            Console.WriteLine("Running DatabaseInitializer Initialize Fuction...");
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                   IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customer')
                                BEGIN
                                    CREATE TABLE Customer (
                                        CustomerId GUID PRIMARY KEY,
                                        FulltName NVARCHAR(50) NOT NULL,
                                        Address NVARCHAR(50) NOT NULL,
                                        PhoneNumber int,
                                        Dob DATE NOT NULL
                                    );
                                END;
                             
                ";
                command.ExecuteNonQuery();
            }
        }

    }
}
