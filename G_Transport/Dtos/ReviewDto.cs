using G_Transport.Models.Domain;

namespace G_Transport.Dtos
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid CustomerId { get; set; }
        public Guid TripId { get; set; }
    }
    public class CreateReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid TripId { get; set; }
    }
}
