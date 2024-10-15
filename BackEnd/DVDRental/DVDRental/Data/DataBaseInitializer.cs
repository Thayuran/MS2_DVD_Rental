using Microsoft.Data.SqlClient;

namespace DVDRental.Data
{
    public class DataBaseInitializer
    {
        private readonly string _connectionString;

        public DataBaseInitializer(string connectionString)
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
                   IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DVDs')
                                BEGIN
                                    CREATE TABLE DVDs (
                                        ID VARCHAR(8) PRIMARY KEY,
                                        Title VARCHAR(50) NOT NULL,
                                        Director VARCHAR(50) NOT NULL,
                                        Genre VARCHAR(20) NOT NULL,
                                        ReleaseDate DATE NOT NULL,
                                        Copies INT NOT NULL                   
                                    );
                                END;
                             
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}

   