using System.ComponentModel.DataAnnotations.Schema;
using Org.BouncyCastle.Asn1.X509;

namespace G_Transport.Models.Domain
{
    public class Trip : BaseEntity
    {
        public string StartingLocation { get; set; }
        public string Destination { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid VehicleId {  get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public DateTime DepartureDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }

    }
}
