namespace DVDRental.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }

        public string? Email { get; set; }
        public int PhoneNo {  get; set; }
        public string Address { get; set; }
        public int AddressId { get; set; }

        public DateTime joined_date {  get; set; }
        public bool Action { get; set; }
    }
}
