namespace DVDRental.DTOs.ResponseDTO
{
    public class RequestResponseDTO
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string DVDTitle { get; set; }
        public string DVDFormat { get; set; }
        public int? DVDYear { get; set; }
        public string AdminComments { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
