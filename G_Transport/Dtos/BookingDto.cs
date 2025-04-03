using System.ComponentModel.DataAnnotations;
using G_Transport.Models.Domain;
using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public string StartingLocation { get; set; } = default!;
        public string Destination { get; set; } = default!;
        public Status Status { get; set; }
        public Guid TripId { get; set; }
        public Guid CustomerId { get; set; }
        public int SeatNo { get; set; }
    }
    public class  CreateBookingRequestModel
    {
        [Required]
        public string StartingLocation { get; set; } = default!;
        [Required]
        public string Destination { get; set; } = default!;
        public Status Status { get; set; }
        public Guid TripId { get; set; }
    }
}
