namespace DVDRental.Entities
{
    public class MovieDvd
    {
        public string? ID { get; set; }
        public string MovieName { get; set; }
       
        public string Title { get; set; }
        public List<Categories> Categories { get; set; } = new List<Categories>() { };
       
        public DateTime ReleaseDate { get; set; }
        public string Director {  get; set; }
        public int Copies {  get; set; }

        public string ImagePath { get; set; }
        /*  public decimal rentprice {  get; set; }*/

    }
}
