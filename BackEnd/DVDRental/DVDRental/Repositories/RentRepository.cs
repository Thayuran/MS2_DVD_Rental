using DVDRental.Entities;
using Microsoft.Data.SqlClient;

namespace DVDRental.Repositories
{
    public class RentRepository:IRentRepository
    {
        private readonly string _connectionString;

        public RentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Movie_Rent>> GetAllRentalsAsync()
        {
            var rentals = new List<Movie_Rent>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Rentals";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var rental = new Movie_Rent
                        {
                            ID = reader["ID"].ToString(),
                            RentDate = Convert.ToDateTime(reader["RentDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null,
                            AdvancePayment = Convert.ToDecimal(reader["AdvancePayment"]),
                            RentalCharge = Convert.ToDecimal(reader["RentalCharge"]),
                            DelayFine = reader["DelayFine"] != DBNull.Value ? Convert.ToDecimal(reader["DelayFine"]) : (decimal?)null,
                            DamageFine = reader["DamageFine"] != DBNull.Value ? Convert.ToDecimal(reader["DamageFine"]) : (decimal?)null,
                            Balance = Convert.ToDecimal(reader["Balance"]),
                            Status = reader["Status"].ToString(),
                            dvdId = reader["dvdId"].ToString(),
                            customerID = reader["customerID"].ToString()
                        };

                        rentals.Add(rental);
                    }
                }
            }

            return rentals;
        }

        public async Task<Movie_Rent> GetRentalByIdAsync(string id)
        {
            Movie_Rent rental = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Rentals WHERE ID = @ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rental = new Movie_Rent
                            {
                                ID = reader["ID"].ToString(),
                                RentDate = Convert.ToDateTime(reader["RentDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null,
                                AdvancePayment = Convert.ToDecimal(reader["AdvancePayment"]),
                                RentalCharge = Convert.ToDecimal(reader["RentalCharge"]),
                                DelayFine = reader["DelayFine"] != DBNull.Value ? Convert.ToDecimal(reader["DelayFine"]) : (decimal?)null,
                                DamageFine = reader["DamageFine"] != DBNull.Value ? Convert.ToDecimal(reader["DamageFine"]) : (decimal?)null,
                                Balance = Convert.ToDecimal(reader["Balance"]),
                                Status = reader["Status"].ToString(),
                                dvdId = reader["dvdId"].ToString(),
                                customerID = reader["customerID"].ToString()
                            };
                        }
                    }
                }
            }

            return rental;
        }

        public async Task<Movie_Rent> AddRentalAsync(Movie_Rent rental)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO Rentals (ID, RentDate, DueDate, ReturnDate, AdvancePayment, 
                            RentalCharge, DelayFine, DamageFine, Balance, Status, dvdId, customerID)
                            VALUES (@ID, @RentDate, @DueDate, @ReturnDate, @AdvancePayment, 
                                    @RentalCharge, @DelayFine, @DamageFine, @Balance, @Status, @dvdId, @customerID)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", rental.ID);
                    command.Parameters.AddWithValue("@RentDate", rental.RentDate);
                    command.Parameters.AddWithValue("@DueDate", rental.DueDate);
                    command.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate.HasValue ? rental.ReturnDate : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AdvancePayment", rental.AdvancePayment);
                    command.Parameters.AddWithValue("@RentalCharge", rental.RentalCharge);
                    command.Parameters.AddWithValue("@DelayFine", rental.DelayFine.HasValue ? rental.DelayFine : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DamageFine", rental.DamageFine.HasValue ? rental.DamageFine : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Balance", rental.Balance);
                    command.Parameters.AddWithValue("@Status", rental.Status);
                    command.Parameters.AddWithValue("@dvdId", rental.dvdId);
                    command.Parameters.AddWithValue("@customerID", rental.customerID);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return rental;
        }

        public async Task UpdateRentalAsync(Movie_Rent rental)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE Rentals SET RentDate = @RentDate, DueDate = @DueDate, 
                             ReturnDate = @ReturnDate, AdvancePayment = @AdvancePayment, 
                             RentalCharge = @RentalCharge, DelayFine = @DelayFine, 
                             DamageFine = @DamageFine, Balance = @Balance, Status = @Status, 
                             dvdId = @dvdId, customerID = @customerID WHERE ID = @ID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", rental.ID);
                    command.Parameters.AddWithValue("@RentDate", rental.RentDate);
                    command.Parameters.AddWithValue("@DueDate", rental.DueDate);
                    command.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate.HasValue ? rental.ReturnDate : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AdvancePayment", rental.AdvancePayment);
                    command.Parameters.AddWithValue("@RentalCharge", rental.RentalCharge);
                    command.Parameters.AddWithValue("@DelayFine", rental.DelayFine.HasValue ? rental.DelayFine : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DamageFine", rental.DamageFine.HasValue ? rental.DamageFine : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Balance", rental.Balance);
                    command.Parameters.AddWithValue("@Status", rental.Status);
                    command.Parameters.AddWithValue("@dvdId", rental.dvdId);
                    command.Parameters.AddWithValue("@customerID", rental.customerID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Movie_Rent>> GetOverdueRentalsAsync()
        {
            var overdueRentals = new List<Movie_Rent>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Rentals WHERE DueDate < @Today AND ReturnDate IS NULL";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Today", DateTime.Now);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var rental = new Movie_Rent
                            {
                                ID = reader["ID"].ToString(),
                                RentDate = Convert.ToDateTime(reader["RentDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null,
                                AdvancePayment = Convert.ToDecimal(reader["AdvancePayment"]),
                                RentalCharge = Convert.ToDecimal(reader["RentalCharge"]),
                                DelayFine = reader["DelayFine"] != DBNull.Value ? Convert.ToDecimal(reader["DelayFine"]) : (decimal?)null,
                                DamageFine = reader["DamageFine"] != DBNull.Value ? Convert.ToDecimal(reader["DamageFine"]) : (decimal?)null,
                                Balance = Convert.ToDecimal(reader["Balance"]),
                                Status = reader["Status"].ToString(),
                                dvdId = reader["dvdId"].ToString(),
                                customerID = reader["customerID"].ToString()
                            };

                            overdueRentals.Add(rental);
                        }
                    }
                }
            }

            return overdueRentals;
        }
    }
}
