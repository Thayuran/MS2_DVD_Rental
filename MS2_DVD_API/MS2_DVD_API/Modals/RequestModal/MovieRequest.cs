namespace MS2_DVD_API.Modals.RequestModal
{
    public class MovieRequest
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Cast { get; set; }
        public int NoOfCopies { get; set; }
    }
}
