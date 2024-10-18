namespace DVDRental.DTOs.ResponseDTO
{
    public class CustomerResponseDTO
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool Action { get; set; }
    }
}
