using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class FixDepartureTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Trips",
                type: "time(6)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverNo",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Trips");

            migrationBuilder.AlterColumn<string>(
                name: "DriverNo",
                table: "Drivers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
