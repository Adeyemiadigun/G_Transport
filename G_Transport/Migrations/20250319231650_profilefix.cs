using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_Transport.Migrations
{
    /// <inheritdoc />
    public partial class profilefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Profiles",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Profiles",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

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
    }
}
