using System.ComponentModel.DataAnnotations;
using G_Transport.Models.Domain;

namespace G_Transport.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public required string Email { get; set; }
        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
    public class LoginRequestModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }

    }
}
