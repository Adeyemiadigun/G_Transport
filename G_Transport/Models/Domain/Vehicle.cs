namespace G_Transport.Models.Domain
{
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string PlateNo { get; set; }
        public Driver Driver { get; set; }
        public Guid DriverId { get; set; }
        public ICollection<Trip> trips { get; set; } = new HashSet<Trip>();
    }
}
