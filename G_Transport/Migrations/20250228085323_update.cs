using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("03ebf444-4a25-4fef-8f51-177feab5332d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28e19445-b5b8-4c78-8444-833157d20486"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("36509326-9ef1-4784-8820-cf9319b6614e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("342a5d9a-614b-4148-a979-ab212c979a7c"), new DateTime(2025, 2, 28, 8, 53, 23, 60, DateTimeKind.Utc).AddTicks(1345), "Regular customer", false, "Customer" },
                    { new Guid("43dd9da9-5965-4408-9a62-7ff3fe0a300f"), new DateTime(2025, 2, 28, 8, 53, 23, 60, DateTimeKind.Utc).AddTicks(1342), "Registered driver", false, "Driver" },
                    { new Guid("d73660ae-4e60-45fe-998c-8fb9761cb5a1"), new DateTime(2025, 2, 28, 8, 53, 23, 60, DateTimeKind.Utc).AddTicks(1336), "System administrator", false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("342a5d9a-614b-4148-a979-ab212c979a7c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("43dd9da9-5965-4408-9a62-7ff3fe0a300f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d73660ae-4e60-45fe-998c-8fb9761cb5a1"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("03ebf444-4a25-4fef-8f51-177feab5332d"), new DateTime(2025, 2, 28, 8, 47, 8, 348, DateTimeKind.Utc).AddTicks(9312), "System administrator", false, "Admin" },
                    { new Guid("28e19445-b5b8-4c78-8444-833157d20486"), new DateTime(2025, 2, 28, 8, 47, 8, 348, DateTimeKind.Utc).AddTicks(9321), "Registered driver", false, "Driver" },
                    { new Guid("36509326-9ef1-4784-8820-cf9319b6614e"), new DateTime(2025, 2, 28, 8, 47, 8, 348, DateTimeKind.Utc).AddTicks(9324), "Regular customer", false, "Customer" }
                });
        }
    }
}
