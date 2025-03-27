using G_Transport.Models.Enums;

namespace G_Transport.Models.Domain
{
    public class Booking : BaseEntity
    {
        public string StartingLocation { get; set; } = default!;
        public string Destination { get; set; } = default!;
        public Status Status { get; set; }
        public Guid TripId {  get; set; }
        public Trip? Trip { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
