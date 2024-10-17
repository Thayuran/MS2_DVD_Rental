namespace MS2_DVD_API.Entity
{
    public class Movies
    {
        public int movieID {  get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public string Cast {  get; set; }
        public int NoOfCopies {  get; set; }
    }
}
