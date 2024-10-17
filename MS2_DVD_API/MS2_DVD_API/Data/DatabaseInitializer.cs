using System.Security.Principal;
using System;
using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
           
                IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Movies')
                BEGIN
                    CREATE TABLE Movies(
                        movieID INT PRIMARY KEY IDENTITY(1,1), 
                        Title NVARCHAR(100) NOT NULL,
                        Genre NVARCHAR(50) NOT NULL,
                        Director NVARCHAR(100) NOT NULL,
                        ReleaseDate DATETIME2 NOT NULL,   
                        Cast NVARCHAR(MAX), 
                        NoOfCopies INT NOT NULL
                    );
                END;
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}
