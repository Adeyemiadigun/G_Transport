namespace G_Transport.Models.Domain
{
    public class TripDriver :BaseEntity
    {
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; } = default!;

        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = default!;
    }

}
