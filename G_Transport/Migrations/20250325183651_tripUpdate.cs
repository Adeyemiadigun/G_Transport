using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class tripUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("15b2682f-3064-441a-ab94-13778f712ac0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("723e3fea-e581-4674-ba13-0cc036030679"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("da56255e-fdd0-4339-a451-170e47784276"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("03f5f273-5f82-4482-b5a2-2ef99f19c5f6"), new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7381), "Regular customer", false, "Customer" },
                    { new Guid("f1c18b3e-a57d-4536-936d-1ff09af181d0"), new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7375), "Registered driver", false, "Driver" },
                    { new Guid("fa515cae-23c6-4750-ac32-e9909f8473f6"), new DateTime(2025, 3, 25, 18, 36, 49, 860, DateTimeKind.Utc).AddTicks(7343), "System administrator", false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("03f5f273-5f82-4482-b5a2-2ef99f19c5f6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f1c18b3e-a57d-4536-936d-1ff09af181d0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fa515cae-23c6-4750-ac32-e9909f8473f6"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("15b2682f-3064-441a-ab94-13778f712ac0"), new DateTime(2025, 3, 25, 18, 31, 40, 890, DateTimeKind.Utc).AddTicks(6807), "System administrator", false, "Admin" },
                    { new Guid("723e3fea-e581-4674-ba13-0cc036030679"), new DateTime(2025, 3, 25, 18, 31, 40, 890, DateTimeKind.Utc).AddTicks(6823), "Registered driver", false, "Driver" },
                    { new Guid("da56255e-fdd0-4339-a451-170e47784276"), new DateTime(2025, 3, 25, 18, 31, 40, 890, DateTimeKind.Utc).AddTicks(6829), "Regular customer", false, "Customer" }
                });
        }
    }
}
