using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4ebdeb01-fe52-42fb-bd2f-0d5b58c6cf02"), "1", "Admin", "Admin" },
                    { new Guid("79008c6e-88b9-4a7f-899b-1a1c1fc29a9e"), "2", "Teacher", "Teacher" },
                    { new Guid("86bfeabf-e058-4d10-b1ee-d7c992204830"), "3", "Student", "Student" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4ebdeb01-fe52-42fb-bd2f-0d5b58c6cf02"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("79008c6e-88b9-4a7f-899b-1a1c1fc29a9e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("86bfeabf-e058-4d10-b1ee-d7c992204830"));
        }
    }
}
