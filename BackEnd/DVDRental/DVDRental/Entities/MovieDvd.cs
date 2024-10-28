namespace DVDRental.Entities
{
    public class MovieDvd
    {
        public string? ID { get; set; }
        
        public string Title { get; set; }

        public int categoryid {  get; set; }
        public Categories category { get; set; } 
       
        public DateTime ReleaseDate { get; set; }
        public string Director {  get; set; }
        public int Copies {  get; set; }

        public string? ImagePath { get; set; }
        /*  public decimal rentprice {  get; set; }*/

    }
}
