using G_Transport.Models.Domain;

namespace G_Transport.Dtos
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string PlateNo { get; set; }
        public int DriverNo { get; set; }
        public Guid DriverId { get; set; }
    }
    public class RegisterVehicleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string PlateNo { get; set; }
        public Guid DriverId { get; set; }
    }
}
