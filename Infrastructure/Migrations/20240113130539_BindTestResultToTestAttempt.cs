using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BindTestResultToTestAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestResult_Student_StudentId",
                table: "StudentTestResult");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b798493a-ffc6-429f-952e-6f94116eb81c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d3d73b1f-3f49-43e3-84a4-d26647c723d0"));

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentTestResult",
                newName: "StudentAttemptId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTestResult_StudentId",
                table: "StudentTestResult",
                newName: "IX_StudentTestResult_StudentAttemptId");

            migrationBuilder.RenameColumn(
                name: "NumberOfAttemts",
                table: "StudentTestAttempts",
                newName: "NumberOfAttemt");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentTestAttemptId",
                table: "TestsQuestion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAttemts",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestsQuestion_StudentTestAttemptId",
                table: "TestsQuestion",
                column: "StudentTestAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestResult_StudentTestAttempts_StudentAttemptId",
                table: "StudentTestResult",
                column: "StudentAttemptId",
                principalTable: "StudentTestAttempts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_StudentTestAttempts_StudentTestAttemptId",
                table: "TestsQuestion",
                column: "StudentTestAttemptId",
                principalTable: "StudentTestAttempts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestResult_StudentTestAttempts_StudentAttemptId",
                table: "StudentTestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_StudentTestAttempts_StudentTestAttemptId",
                table: "TestsQuestion");

            migrationBuilder.DropIndex(
                name: "IX_TestsQuestion_StudentTestAttemptId",
                table: "TestsQuestion");

            migrationBuilder.DropColumn(
                name: "StudentTestAttemptId",
                table: "TestsQuestion");

            migrationBuilder.DropColumn(
                name: "NumberOfAttemts",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "StudentAttemptId",
                table: "StudentTestResult",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTestResult_StudentAttemptId",
                table: "StudentTestResult",
                newName: "IX_StudentTestResult_StudentId");

            migrationBuilder.RenameColumn(
                name: "NumberOfAttemt",
                table: "StudentTestAttempts",
                newName: "NumberOfAttemts");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestResult_Student_StudentId",
                table: "StudentTestResult",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
