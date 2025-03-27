using G_Transport.Models.Enums;

namespace G_Transport.Models.Domain
{
    public class Payment : BaseEntity
    {
        public string RefrenceNo { get; set; }
        public string Transaction { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
