namespace MS2_DVD_API.Modals.ResponseModal
{
    public class RequestRespone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DvdId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Action { get; set; }
    }
}
