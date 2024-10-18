namespace DVDRental.DTOs.RequestDTO
{
    public class CustomerRequestDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumber { get; set; }
    }
}
