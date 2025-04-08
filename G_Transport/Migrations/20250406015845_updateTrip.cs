using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class updateTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4736f221-f829-4584-9431-52bdf9c10048"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("736eb72f-d382-468f-963f-c70729f5bd45"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a1c51e6e-7306-42ec-87b2-8af3f379bf52"));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Trips",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("4736f221-f829-4584-9431-52bdf9c10048"), new DateTime(2025, 4, 3, 19, 39, 0, 877, DateTimeKind.Utc).AddTicks(1274), "System administrator", false, "Admin" },
                    { new Guid("736eb72f-d382-468f-963f-c70729f5bd45"), new DateTime(2025, 4, 3, 19, 39, 0, 877, DateTimeKind.Utc).AddTicks(1283), "Registered driver", false, "Driver" },
                    { new Guid("a1c51e6e-7306-42ec-87b2-8af3f379bf52"), new DateTime(2025, 4, 3, 19, 39, 0, 877, DateTimeKind.Utc).AddTicks(1287), "Regular customer", false, "Customer" }
                });
        }
    }
}
