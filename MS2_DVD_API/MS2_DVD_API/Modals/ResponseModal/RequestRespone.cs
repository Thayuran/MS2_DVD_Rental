namespace MS2_DVD_API.Modals.ResponseModal
{
    public class RequestRespone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MovieID { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Action { get; set; }
    }
}
