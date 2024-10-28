namespace DVDRental.Entities
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
       /* public string DVDId { get; set; }*/
        public ICollection<MovieDvd> DVDs { get; set; }=new List<MovieDvd>();   
    }
}
