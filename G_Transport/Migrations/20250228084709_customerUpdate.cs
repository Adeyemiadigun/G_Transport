using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class customerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("58149679-9596-4bbf-8079-b40c7f81ce13"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64017410-6a48-4fcc-952b-1f7c045fd517"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("af15ec95-f19a-4cae-846d-896de8b26221"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("58149679-9596-4bbf-8079-b40c7f81ce13"), new DateTime(2025, 2, 28, 8, 29, 19, 884, DateTimeKind.Utc).AddTicks(4263), "Registered driver", false, "Driver" },
                    { new Guid("64017410-6a48-4fcc-952b-1f7c045fd517"), new DateTime(2025, 2, 28, 8, 29, 19, 884, DateTimeKind.Utc).AddTicks(4163), "System administrator", false, "Admin" },
                    { new Guid("af15ec95-f19a-4cae-846d-896de8b26221"), new DateTime(2025, 2, 28, 8, 29, 19, 884, DateTimeKind.Utc).AddTicks(4267), "Regular customer", false, "Customer" }
                });
        }
    }
}
