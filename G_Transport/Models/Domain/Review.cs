namespace G_Transport.Models.Domain
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid CustomerId {  get; set; }
        public Customer Customer { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
