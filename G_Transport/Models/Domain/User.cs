namespace G_Transport.Models.Domain
{
    public class User : BaseEntity
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
}
