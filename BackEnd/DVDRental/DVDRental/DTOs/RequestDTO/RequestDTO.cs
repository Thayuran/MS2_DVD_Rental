using DVDRental.Entities;

namespace DVDRental.DTOs.RequestDTO
{
    public class RequestDTO
    {
       
        public string customerId { get; set; }
        public string movieId { get; set; }

        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
