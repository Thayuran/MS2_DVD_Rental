namespace DVDRental.DTOs.ResponseDTO
{
    public class DVDResponseDTO
    {
        public string ID { get; set; }
        public string MovieName { get; set; }
        public List<int> Categories { get; set; } = new List<int>() { };
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Copies { get; set; }
        public string ImagePath { get; set; }
        /*public decimal RentPrice { get; set; }*/
    }
}
