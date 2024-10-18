namespace DVDRental.Entities
{
    public class Rent_Request
    {
        public string ID {  get; set; }
        public Customer customer { get; set; }
        public string customerId {  get; set; }

        public MovieDvd movie { get; set; }
        public string movieId { get; set; }

        public string Status { get; set; }
        public DateTime RequestDate {  get; set; }
    }
}
