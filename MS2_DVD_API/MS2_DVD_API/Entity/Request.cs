namespace MS2_DVD_API.Entity
{
    public class Request
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public int dvdid { get; set; }
        public DateTime RequestDtae { get; set; }
        public string Action { get; set; }
    }
}
