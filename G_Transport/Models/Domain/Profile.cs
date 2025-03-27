using G_Transport.Models.Enums;
namespace G_Transport.Models.Domain
{
    public class Profile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public Driver? Driver { get; set; }
        public Customer? Customer { get; set; }
    }
}
