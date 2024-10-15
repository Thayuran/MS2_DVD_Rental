namespace DVDRental.Entities
{
    public class MovieDvd
    {
        public string? ID { get; set; }
        public string MovieName { get; set; }
       /* public string MovieDescription { get; set; }*/
        public string Categories { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director {  get; set; }
        public int Copies {  get; set; }
      /*  public decimal rentprice {  get; set; }*/

    }
}
