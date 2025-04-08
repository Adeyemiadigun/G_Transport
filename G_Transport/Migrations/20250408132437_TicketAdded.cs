using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class TicketAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4935d3c1-9d6e-4846-bb5d-d8765b08fd03"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("59964238-da12-40e0-b7aa-cb5c397d78a2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5cf580f1-c5bc-4658-8c3a-418a1c68f279"));

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TripOrigin = table.Column<string>(type: "longtext", nullable: false),
                    TripDestination = table.Column<string>(type: "longtext", nullable: false),
                    TripDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    TicketNumber = table.Column<string>(type: "longtext", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TripId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RefrenceNo = table.Column<string>(type: "longtext", nullable: false),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("0c0bd296-d688-49ba-80bf-3d9cdf6320fe"), new DateTime(2025, 4, 8, 13, 24, 36, 326, DateTimeKind.Utc).AddTicks(496), "System administrator", false, "Admin" },
                    { new Guid("b65cb231-1f89-4aa4-9861-62f1f4dde9a5"), new DateTime(2025, 4, 8, 13, 24, 36, 326, DateTimeKind.Utc).AddTicks(601), "Regular customer", false, "Customer" },
                    { new Guid("fea1b7eb-3fb2-4566-a9bc-df18fbed775b"), new DateTime(2025, 4, 8, 13, 24, 36, 326, DateTimeKind.Utc).AddTicks(518), "Registered driver", false, "Driver" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0c0bd296-d688-49ba-80bf-3d9cdf6320fe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b65cb231-1f89-4aa4-9861-62f1f4dde9a5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fea1b7eb-3fb2-4566-a9bc-df18fbed775b"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("4935d3c1-9d6e-4846-bb5d-d8765b08fd03"), new DateTime(2025, 4, 6, 1, 58, 44, 44, DateTimeKind.Utc).AddTicks(8361), "Registered driver", false, "Driver" },
                    { new Guid("59964238-da12-40e0-b7aa-cb5c397d78a2"), new DateTime(2025, 4, 6, 1, 58, 44, 44, DateTimeKind.Utc).AddTicks(8377), "Regular customer", false, "Customer" },
                    { new Guid("5cf580f1-c5bc-4658-8c3a-418a1c68f279"), new DateTime(2025, 4, 6, 1, 58, 44, 44, DateTimeKind.Utc).AddTicks(8340), "System administrator", false, "Admin" }
                });
        }
    }
}
