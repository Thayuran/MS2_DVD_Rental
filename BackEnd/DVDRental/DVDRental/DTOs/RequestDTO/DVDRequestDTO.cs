namespace DVDRental.DTOs.RequestDTO
{
    public class DVDRequestDTO
    {

        public string Title { get; set; }
        public int CategoryId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Copies { get; set; }
        public IFormFile? Image { get; set; }
        public decimal rentprice { get; set; }
    }
}
