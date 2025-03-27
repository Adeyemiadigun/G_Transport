using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "DepartureTime",
                table: "Trips",
                type: "time",
                nullable: false
                );

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
                    { new Guid("70fef5d2-fe0e-472e-a739-9ace0c314144"), new DateTime(2025, 2, 25, 14, 16, 32, 209, DateTimeKind.Utc).AddTicks(5917), "Regular customer", false, "Customer" },
                    { new Guid("7c696bcc-fdb4-405a-a4e6-1237239a31e2"), new DateTime(2025, 2, 25, 14, 16, 32, 209, DateTimeKind.Utc).AddTicks(5900), "Registered driver", false, "Driver" },
                    { new Guid("acf120e2-a506-4403-94be-0a775f5a1344"), new DateTime(2025, 2, 25, 14, 16, 32, 209, DateTimeKind.Utc).AddTicks(5893), "System administrator", false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("70fef5d2-fe0e-472e-a739-9ace0c314144"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7c696bcc-fdb4-405a-a4e6-1237239a31e2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("acf120e2-a506-4403-94be-0a775f5a1344"));

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
