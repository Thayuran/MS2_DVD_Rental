namespace DVDRental.Entities
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DVDId { get; set; }
        public MovieDvd DVD { get; set; }
    }
}
