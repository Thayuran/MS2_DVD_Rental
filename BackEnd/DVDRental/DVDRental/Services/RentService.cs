using DVDRental.DTOs.RequestDTO;
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

       /* public async Task<List<DVD_RentalDTO>>GetAllDVDRentalsAsync()
        {
            var rentals = await _rentRepository.GetAllRentalsAsync();
            return rentals;*//*.Select(r=>MapToDto(r)).ToList();*//*

        }*/
    }
}
