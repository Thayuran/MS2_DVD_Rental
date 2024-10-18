namespace DVDRental.DTOs.ResponseDTO
{
    public class RequestResponseDTO
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string customerId { get; set; }

        public string movieId { get; set; }
        public DateTime RequestDate { get; set; }
 
        public DateTime? LastUpdated { get; set; }
    }
}
