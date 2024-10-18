using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
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


        public async Task<IEnumerable<RentalResponseDTO>> GetAllRentalsAsync()
        {
            var rentals = await _rentRepository.GetAllRentalsAsync();
            return rentals.Select(r => MapToDto(r));
        }

        public async Task<RentalResponseDTO> GetRentalByIdAsync(string id)
        {
            var rental = await _rentRepository.GetRentalByIdAsync(id);
            return MapToDto(rental);
        }

        public async Task<int> CreateRentalFromRequestAsync(string requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.Status != "Accepted")
                throw new InvalidOperationException("Invalid or unaccepted request");

            var dvd = await _adminDvdRepository.GetMovieById(request.movieId);
               
            if (dvd.AvailableCopies <= 1)
                throw new InvalidOperationException("No available copies of the DVD");

            var rental = new DVDRental
            {
                CustomerId = request.customerId,
                DVDId = request.movieId,
                RentalDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                RentalCharge = Rental_Price,
                AdvancePayment = Advance_Payment,
                Status = "Active"
            };

            var rentalId = await _rentRepository.AddRentalAsync(rental);

            dvd.AvailableCopies--;
            await _adminDvdRepository.UpdateDVDAsync(dvd);

            request.Status = "Completed";
            await _requestRepository.UpdateRequestAsync(request);

            return rentalId;
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

            var dvd = await _adminDvdRepository.GetDVDByIdAsync(rental.DVDId);
            dvd.AvailableCopies++;
            await _adminDvdRepository.UpdateDVDAsync(dvd);
        }

        public async Task<IEnumerable<RentalResponseDTO>> GetOverdueRentalsAsync()
        {
            var overdueRentals = await _rentRepository.GetOverdueRentalsAsync();
            return overdueRentals.Select(r => MapToDto(r));
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

        private RentalDto MapToDto(Rental rental)
        {
            return new RentalDto
            {
                Id = rental.Id,
                CustomerId = rental.CustomerId,
                DVDId = rental.DVDId,
                RentalDate = rental.RentalDate,
                DueDate = rental.DueDate,
                ReturnDate = rental.ReturnDate,
                RentalCharge = rental.RentalCharge,
                AdvancePayment = rental.AdvancePayment,
                DelayFine = rental.DelayFine,
                DamageFine = rental.DamageFine,
                Status = rental.Status
            };
        }
    }
}
