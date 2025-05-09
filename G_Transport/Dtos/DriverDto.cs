﻿using System.ComponentModel.DataAnnotations;
using G_Transport.Models.Domain;
using G_Transport.Models.Enums;

namespace G_Transport.Dtos
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        public int DriverNo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class RegisterDriverRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        public string? Address { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
