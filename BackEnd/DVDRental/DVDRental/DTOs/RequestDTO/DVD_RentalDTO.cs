using DVDRental.Entities;

namespace DVDRental.DTOs.RequestDTO
{
    public class DVD_RentalDTO
    {
        public int Id { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal AdvancePayment { get; set; }

        public decimal RentalCharge { get; set; }

        public decimal? DelayFine { get; set; }
        public decimal? DamageFine { get; set; }

        public decimal Balance { get; set; }
        public string Status { get; set; }
        public int dvdId { get; set; }
        public int customerId { get; set; }
    }
}
