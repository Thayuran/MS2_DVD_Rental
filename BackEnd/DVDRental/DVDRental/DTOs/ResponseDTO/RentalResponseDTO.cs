namespace DVDRental.DTOs.ResponseDTO
{
    public class RentalResponseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DVDId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal RentalCharge { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal? DelayFine { get; set; }
        public decimal? DamageFine { get; set; }
        public string Status { get; set; }
    }
}
