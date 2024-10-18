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
                                    CREATE TABLE DVDs(
                                        ID VARCHAR(8) PRIMARY KEY,
                                        Title VARCHAR(50) NOT NULL,
                                        Director VARCHAR(50) NOT NULL,
                                        ReleaseDate DATE NOT NULL,
                                        Copies INT NOT NULL,
                                        ImagePath NVARCHAR(255) 
                                    );
                                END;

                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name ='Categories')
                                    BEGIN
                                        CREATE TABLE Categories (
                                            CategoryId INT PRIMARY KEY IDENTITY(1,1),
                                            Name NVARCHAR(50) NOT NULL UNIQUE
                                        );
                                    END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DVD_Categories')
                                     BEGIN
                                        CREATE TABLE DVD_Categories (
                                            DVDId VARCHAR(8) NOT NULL,
                                            CategoryId INT NOT NULL,
                                            PRIMARY KEY (DVDId,CategoryId),
                                            FOREIGN KEY (DVDId) REFERENCES DVDs(ID) ON DELETE CASCADE,
                                            FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
                                        );
                                        END;


                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customer')
                                    BEGIN
                                        CREATE TABLE Customer (
                                            Id NVARCHAR(10) PRIMARY KEY, 
                                            CustomerName NVARCHAR(50) NOT NULL,
                                            Email NVARCHAR(100) NOT NULL,
                                            Address NVARCHAR(100) NOT NULL,
                                            AddressId INT NOT NULL,
                                            PhoneNo INT NOT NULL, 
                                            JoinedDate DATETIME2 NOT NULL,
                                            Action BIT NOT NULL
                                        );
                                    END;

 IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Requests')
                                    BEGIN
                                        CREATE TABLE Requests (
                                            ID NVARCHAR(10) PRIMARY KEY,
                                            customerId NVARCHAR(10) NOT NULL,
                                            movieId VARCHAR(8) NOT NULL,
                                            RequestDate DATETIME NOT NULL,
                                            Status NVARCHAR(20) NOT NULL, -- 'Pending', 'Accepted', 'Rejected'
                                            FOREIGN KEY (customerId) REFERENCES Customer(Id),
                                            FOREIGN KEY (movieId) REFERENCES DVDs(ID)
                                        );
                                    END;

 IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Rentals')
                                  BEGIN
                                    CREATE TABLE Rentals (
                                        ID NVARCHAR(20) PRIMARY KEY,
                                        customerID NVARCHAR(10) NOT NULL,
                                        dvdId VARCHAR(8) NOT NULL,
                                        RentDate DATE NOT NULL,
                                        DueDate DATE NOT NULL,
                                        ReturnDate DATE,
                                        RentalCharge DECIMAL(10, 2) NOT NULL,
                                        AdvancePayment DECIMAL(10, 2) NOT NULL,
                                        DelayFine DECIMAL(10, 2),
                                        DamageFine DECIMAL(10, 2),
                                        Balance DECIMAL(10, 2),
                                        Status NVARCHAR(20) NOT NULL, -- 'Active', 'Returned', 'Overdue'
                                        FOREIGN KEY (customerID) REFERENCES Customer(Id),
                                        FOREIGN KEY (dvdId) REFERENCES DVDs(ID)
                                    );
                                 END;

                             
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}

   