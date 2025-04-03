using System.ComponentModel.DataAnnotations;
using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }

    }
    public class RegisterCUstomerRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
