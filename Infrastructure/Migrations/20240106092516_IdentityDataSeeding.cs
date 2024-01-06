using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("29ff746b-a60d-4032-8af7-5fc73c2b29f8"), "2", "Teacher", "Teacher" },
                    { new Guid("65216587-6548-41ca-bcce-dc80fac21a66"), "1", "Admin", "Admin" },
                    { new Guid("e4834cbb-8f48-40b4-b1b2-6c4ff72bcd03"), "3", "Student", "Student" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fbc05411-351e-4e79-b971-2246fc3a9125"), 0, "a2ed1afb-10ac-42de-ae45-07792381d0c9", "admin@gmail.com", false, false, null, "admin", null, "AQAAAAIAAYagAAAAEGX50lNBovSWsl6VmrY/ioUpfONHw4uvRZUZuUeJyfEbgS7Eh64O7KZQDaw6ySrnWA==", null, false, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("65216587-6548-41ca-bcce-dc80fac21a66"), new Guid("fbc05411-351e-4e79-b971-2246fc3a9125") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("29ff746b-a60d-4032-8af7-5fc73c2b29f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e4834cbb-8f48-40b4-b1b2-6c4ff72bcd03"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("65216587-6548-41ca-bcce-dc80fac21a66"), new Guid("fbc05411-351e-4e79-b971-2246fc3a9125") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("65216587-6548-41ca-bcce-dc80fac21a66"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fbc05411-351e-4e79-b971-2246fc3a9125"));
        }
    }
}
