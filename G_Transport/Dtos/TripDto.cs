using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public string StartingLocation { get; set; }
        public string Destination { get; set; }
        public Status Status { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid VehicleId { get; set; }
        public string VehicleName { get; set; }
        public ICollection<DriverDto> Drivers { get; set; } = new HashSet<DriverDto>();
    }
    public class CreateTripRequestModel
    {
        public string StartingLocation { get; set; }
        public string Destination { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid VehicleId { get; set; }
        public ICollection<Guid> DriverIds { get; set; } = new HashSet<Guid>();
    }
    public class UpdateTripRequestModel
    {
        public Guid Id { get; set; }
        public string StartingLocation { get; set; }
        public string Destination { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid VehicleId { get; set; }
        public ICollection<Guid> DriverIds { get; set; } = new HashSet<Guid>();
    }
    public class TriggerTripStatus
    {
        public Guid Id { get; set; }    
        public Status Status { get; set; }
    }

}
