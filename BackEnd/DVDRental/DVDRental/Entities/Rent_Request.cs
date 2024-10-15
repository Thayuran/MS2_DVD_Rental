namespace DVDRental.Entities
{
    public class Rent_Request
    {
        public int ID {  get; set; }
        public Customer customer { get; set; }
        public int customerId {  get; set; }

        public MovieDvd movie { get; set; }
        public int movieId { get; set; }

        public string Status { get; set; }
        public DateTime RequestDate {  get; set; }
    }
}
