using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class profilefixUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("37a07e1e-42e7-46c5-a53a-f3a65fdd79e4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dff82aa-d59f-448f-8b28-9004eb27781f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b480a45-6841-452b-8932-eafa8b016ba4"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("23a61076-ee82-4edf-a8cf-c3f9a1d13388"), new DateTime(2025, 3, 19, 23, 32, 29, 484, DateTimeKind.Utc).AddTicks(3692), "System administrator", false, "Admin" },
                    { new Guid("739066ed-ccec-40a4-bc7f-9d4349037e43"), new DateTime(2025, 3, 19, 23, 32, 29, 484, DateTimeKind.Utc).AddTicks(3722), "Regular customer", false, "Customer" },
                    { new Guid("836f43dc-d0e8-483a-aace-80c453df9894"), new DateTime(2025, 3, 19, 23, 32, 29, 484, DateTimeKind.Utc).AddTicks(3718), "Registered driver", false, "Driver" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("23a61076-ee82-4edf-a8cf-c3f9a1d13388"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("739066ed-ccec-40a4-bc7f-9d4349037e43"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("836f43dc-d0e8-483a-aace-80c453df9894"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("37a07e1e-42e7-46c5-a53a-f3a65fdd79e4"), new DateTime(2025, 3, 19, 23, 16, 43, 958, DateTimeKind.Utc).AddTicks(3650), "System administrator", false, "Admin" },
                    { new Guid("3dff82aa-d59f-448f-8b28-9004eb27781f"), new DateTime(2025, 3, 19, 23, 16, 43, 958, DateTimeKind.Utc).AddTicks(3669), "Regular customer", false, "Customer" },
                    { new Guid("4b480a45-6841-452b-8932-eafa8b016ba4"), new DateTime(2025, 3, 19, 23, 16, 43, 958, DateTimeKind.Utc).AddTicks(3663), "Registered driver", false, "Driver" }
                });
        }
    }
}
