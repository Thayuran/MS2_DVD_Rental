using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;
using DVDRental.Repositories;

namespace DVDRental.Services
{
    public class RentService:IRentService
    {
        public readonly IRentRepository _rentRepository;
        public readonly IRequestRepository _requestRepository;
        public readonly IAdminDvdRepository _adminDvdRepository;

        public const decimal Rental_Price = 80;
        public const decimal Advance_Payment = 200;
        public const decimal Delay_Fine = 5;
        public const decimal Damage_Fine = 50;
        
        
        public RentService() { }

        public RentService(IRentRepository rentRepository, IRequestRepository requestRepository, IAdminDvdRepository adminDvdRepository)
        {
            _rentRepository = rentRepository;
            _requestRepository = requestRepository;
            _adminDvdRepository = adminDvdRepository;
        }


        public async Task<List<RentalResponseDTO>> GetAllRentalsAsync()
        {
            var rentals = await _rentRepository.GetAllRentalsAsync();
            
            return rentals.Select(rental => new RentalResponseDTO
            {
                Id = rental.ID,
                CustomerId = rental.customerID,
                DVDId = rental.dvdId,
                RentalDate = rental.RentDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                RentalCharge = rental.RentalCharge,
                AdvancePayment = rental.AdvancePayment,
                DelayFine = rental.DelayFine,
                DamageFine = rental.DamageFine,
                Status = rental.Status
            }).ToList();
        }

        public async Task<RentalResponseDTO> GetRentalByIdAsync(string id)
        {
            var rental = await _rentRepository.GetRentalByIdAsync(id);
            if (rental == null)
            {
                throw new KeyNotFoundException($"Rental with ID {id} not found.");
            }

            return new RentalResponseDTO
            {
                Id = rental.ID,
                CustomerId = rental.customerID,
                DVDId = rental.dvdId,
                RentalDate = rental.RentDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                RentalCharge = rental.RentalCharge,
                AdvancePayment = rental.AdvancePayment,
                DelayFine = rental.DelayFine,
                DamageFine = rental.DamageFine,
                Status = rental.Status
            };
        }

        public async Task<RentalResponseDTO> CreateRentalFromRequestAsync(string requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.Status != "Accepted")
                throw new InvalidOperationException("Invalid or unaccepted request");

            var dvd = await _adminDvdRepository.GetMovieById(request.movieId);
               
            if (dvd.Copies <= 1)
                throw new InvalidOperationException("No available copies of the DVD");

            var rental = new Movie_Rent
            {
                customerID = request.customerId,
                dvdId = request.movieId,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                RentalCharge = Rental_Price,
                AdvancePayment = Advance_Payment,
                Status = "Rented"
            };

            var rentaldvd = await _rentRepository.AddRentalAsync(rental);

            dvd.Copies--;
            await _adminDvdRepository.UpdateAsync(dvd);

            request.Status = "Completed";
            await _requestRepository.UpdateRequestAsync(request);

            return new RentalResponseDTO
            {
                Id = rentaldvd.ID,
                CustomerId = rentaldvd.customerID,
                DVDId = rentaldvd.dvdId,
                RentalDate = rentaldvd.RentDate,
                DueDate = rentaldvd.DueDate,
                RentalCharge = rentaldvd.RentalCharge,
                AdvancePayment = rentaldvd.AdvancePayment,
                Status = rentaldvd.Status
            };
        }

        public async Task ReturnDVDAsync(string rentalId, bool isDamaged)
        {
            var rental = await _rentRepository.GetRentalByIdAsync(rentalId);
            if (rental == null || rental.Status != "Active")
                throw new InvalidOperationException("Invalid or inactive rental");

            rental.ReturnDate = DateTime.Now;
            rental.Status = "Returned";

            if (isDamaged)
            {
                rental.DamageFine = Damage_Fine;
            }

            if (rental.ReturnDate > rental.DueDate)
            {
                int daysLate = (int)(rental.ReturnDate.Value - rental.DueDate).TotalDays;
                rental.DelayFine = daysLate * Delay_Fine;
            }

            await _rentRepository.UpdateRentalAsync(rental);

            var dvd = await _adminDvdRepository.GetMovieById(rental.dvdId);
            dvd.Copies++;
            await _adminDvdRepository.UpdateAsync(dvd);
        }

        public async Task<List<RentalResponseDTO>> GetOverdueRentalsAsync()
        {
            var overdueRentals = await _rentRepository.GetOverdueRentalsAsync();

            return overdueRentals.Select(rental => new RentalResponseDTO
            {
                Id = rental.ID,
                CustomerId = rental.customerID,
                DVDId = rental.dvdId,
                RentalDate = rental.RentDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                RentalCharge = rental.RentalCharge,
                AdvancePayment = rental.AdvancePayment,
                DelayFine = rental.DelayFine,
                DamageFine = rental.DamageFine,
                Status = rental.Status
            }).ToList();
        }

        public async Task CalculateAndApplyFinesAsync()
        {
            var overdueRentals = await _rentRepository.GetOverdueRentalsAsync();
            foreach (var rental in overdueRentals)
            {
                int daysLate = (int)(DateTime.Now - rental.DueDate).TotalDays;
                rental.DelayFine = daysLate * Delay_Fine;
                rental.Status = "Overdue";
                await _rentRepository.UpdateRentalAsync(rental);
            }
        }

        /*private RentalResponseDTO MapToDto(Movie_Rent rental)
        {
            return new RentalResponseDTO
            {
                Id = rental.ID,
                CustomerId = rental.customerID,
                DVDId = rental.dvdId,
                RentalDate = rental.RentDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                RentalCharge = rental.RentalCharge,
                AdvancePayment = rental.AdvancePayment,
                DelayFine = rental.DelayFine,
                DamageFine = rental.DamageFine,
                Status = rental.Status
            };
        }*/
    }
}
