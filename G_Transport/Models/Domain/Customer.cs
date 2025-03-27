namespace G_Transport.Models.Domain
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid ProfileId { get; set; } = default!;
        public Profile Profile { get; set; } = default
            !;
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
