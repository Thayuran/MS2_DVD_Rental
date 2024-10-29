namespace MS2_DVD_API.Modals.RequestModal
{
    public class RequestRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DvdId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Action { get; set; }
    }
}
