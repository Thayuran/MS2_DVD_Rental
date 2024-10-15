using DVDRental.Entities;

namespace DVDRental.DTOs.RequestDTO
{
    public class DVD_RequestDTO
    {
        public int ID { get; set; }
        public int customerId { get; set; }
        public int movieId { get; set; }

        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
