using G_Transport.Models.Domain;
using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class ProfileDto
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? ImageUrl { get; set; }
        public Gender Gender { get; set; }
    }
    public class UpdateProfileRequestModel
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? ImageUrl { get; set; }
    }

}
