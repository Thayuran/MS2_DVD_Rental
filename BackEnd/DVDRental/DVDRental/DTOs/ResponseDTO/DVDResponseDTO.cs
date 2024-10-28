namespace DVDRental.DTOs.ResponseDTO
{
    public class DVDResponseDTO
    {
        public string ID { get; set; }
        public string MovieName { get; set; }
        public int CategoryID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Copies { get; set; }
        public string ImagePath { get; set; }
        /*public decimal RentPrice { get; set; }*/
    }
}
