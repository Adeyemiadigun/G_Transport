using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerPaymentsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7521a4ee-e514-46cd-ad6d-a553023f7a61"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d06c9e0d-5d14-4e35-b266-462d6fc14192"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f78da087-8308-4513-8d4a-780e631fae6c"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("44d1f16f-35f3-4e8a-a8b7-47f267880462"), new DateTime(2025, 2, 27, 22, 19, 13, 789, DateTimeKind.Utc).AddTicks(7224), "Regular customer", false, "Customer" },
                    { new Guid("780fe14e-fd30-42a1-bd05-18a92368585c"), new DateTime(2025, 2, 27, 22, 19, 13, 789, DateTimeKind.Utc).AddTicks(7216), "Registered driver", false, "Driver" },
                    { new Guid("cfc9d8ff-6ba5-454e-ad41-949da12af8c8"), new DateTime(2025, 2, 27, 22, 19, 13, 789, DateTimeKind.Utc).AddTicks(7182), "System administrator", false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44d1f16f-35f3-4e8a-a8b7-47f267880462"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("780fe14e-fd30-42a1-bd05-18a92368585c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cfc9d8ff-6ba5-454e-ad41-949da12af8c8"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("7521a4ee-e514-46cd-ad6d-a553023f7a61"), new DateTime(2025, 2, 25, 14, 55, 55, 69, DateTimeKind.Utc).AddTicks(9224), "Regular customer", false, "Customer" },
                    { new Guid("d06c9e0d-5d14-4e35-b266-462d6fc14192"), new DateTime(2025, 2, 25, 14, 55, 55, 69, DateTimeKind.Utc).AddTicks(9220), "Registered driver", false, "Driver" },
                    { new Guid("f78da087-8308-4513-8d4a-780e631fae6c"), new DateTime(2025, 2, 25, 14, 55, 55, 69, DateTimeKind.Utc).AddTicks(9214), "System administrator", false, "Admin" }
                });
        }
    }
}
