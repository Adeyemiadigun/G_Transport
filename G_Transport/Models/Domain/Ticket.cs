namespace G_Transport.Models.Domain
{
    public class Ticket : BaseEntity
    {
        public string TripOrigin { get; set; }
        public string TripDestination { get; set; }

        public DateTime TripDate { get; set; }
        public int SeatNumber { get; set; }

        public string TicketNumber { get; set; }

        public decimal AmountPaid { get; set; }
        public Guid TripId { get; set; }
        public string RefrenceNo { get; set; }
        public Guid CustomerId { get; set; }
    }
}
