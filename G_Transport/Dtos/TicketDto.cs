using System.ComponentModel.DataAnnotations;

namespace G_Transport.Dtos
{
    public class TicketDto
    {
        [Required]
        public Guid Id { get; set; }
        public string TripOrigin { get; set; }

        [Required]
        public string TripDestination { get; set; }

        [Required]
        public DateTime TripDate { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public string TransactionId { get; set; }
    }
    public class TicketRequest
    {
        [Required]
        public string TripOrigin { get; set; }

        [Required]
        public string TripDestination { get; set; }

        [Required]
        public DateTime TripDate { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public string TransactionId { get; set; }
    }

}
