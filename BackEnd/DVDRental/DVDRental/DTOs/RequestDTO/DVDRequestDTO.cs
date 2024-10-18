namespace DVDRental.DTOs.RequestDTO
{
    public class DVDRequestDTO
    {

        public string Title { get; set; }
        public List<int> CategoryIds { get; set; }=new List<int>(){ };
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Copies { get; set; }
        public IFormFile? Image { get; set; }
    }
}
