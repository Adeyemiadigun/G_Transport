using G_Transport.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Context
{
    public class G_TransportContext : DbContext
    {
        public G_TransportContext(DbContextOptions<G_TransportContext> opt) : base(opt) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Name = "Admin", Description = "System administrator" },
                new Role { Id = Guid.NewGuid(), Name = "Driver", Description = "Registered driver" },
                new Role { Id = Guid.NewGuid(), Name = "Customer", Description = "Regular customer" }
            );
        }
    }

}
