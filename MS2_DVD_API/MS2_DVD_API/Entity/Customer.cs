namespace MS2_DVD_API.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AddressId { get; set; }
        public int phoneNumber { get; set; }
        public DateTime joined_date { get; set; }
        public bool Action {  get; set; }   
    }

}
