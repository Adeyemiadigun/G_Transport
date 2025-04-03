namespace G_Transport.Models.Domain
{
    public class Driver : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int DriverNo { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        
    }
}
