﻿// <auto-generated />
using System;
using G_Transport.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace G_Transport.Migrations
{
    [DbContext(typeof(G_TransportContext))]
    [Migration("20250325183651_tripUpdate")]
    partial class tripUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("G_Transport.Models.Domain.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("StartingLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TripId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DriverNo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefrenceNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Transaction")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TripId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("TripId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa515cae-23c6-4750-ac32-e9909f8473f6"),
                            DateCreated = new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7343),
                            Description = "System administrator",
                            IsDeleted = false,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("f1c18b3e-a57d-4536-936d-1ff09af181d0"),
                            DateCreated = new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7375),
                            Description = "Registered driver",
                            IsDeleted = false,
                            Name = "Driver"
                        },
                        new
                        {
                            Id = new Guid("03f5f273-5f82-4482-b5a2-2ef99f19c5f6"),
                            DateCreated = new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7381),
                            Description = "Regular customer",
                            IsDeleted = false,
                            Name = "Customer"
                        });
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly>("DepartureDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("DepartureTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("StartingLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PlateNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("WalletBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Booking", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G_Transport.Models.Domain.Trip", "Trip")
                        .WithMany("Bookings")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Customer", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Profile", "Profile")
                        .WithOne("Customer")
                        .HasForeignKey("G_Transport.Models.Domain.Customer", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Driver", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Profile", "Profile")
                        .WithOne("Driver")
                        .HasForeignKey("G_Transport.Models.Domain.Driver", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Payment", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G_Transport.Models.Domain.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Review", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G_Transport.Models.Domain.Trip", "Trip")
                        .WithMany("Reviews")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Trip", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Vehicle", "Vehicle")
                        .WithMany("trips")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.UserRole", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G_Transport.Models.Domain.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Vehicle", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Driver", "Driver")
                        .WithOne("Vehicle")
                        .HasForeignKey("G_Transport.Models.Domain.Vehicle", "DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Wallet", b =>
                {
                    b.HasOne("G_Transport.Models.Domain.Customer", "Customer")
                        .WithOne("Wallet")
                        .HasForeignKey("G_Transport.Models.Domain.Wallet", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Customer", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Payments");

                    b.Navigation("Reviews");

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Driver", b =>
                {
                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Profile", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Trip", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.User", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("G_Transport.Models.Domain.Vehicle", b =>
                {
                    b.Navigation("trips");
                });
#pragma warning restore 612, 618
        }
    }
}
