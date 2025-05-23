﻿namespace G_Transport.Models.Domain
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
