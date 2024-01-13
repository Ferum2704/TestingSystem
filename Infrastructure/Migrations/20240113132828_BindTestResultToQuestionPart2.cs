using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BindTestResultToQuestionPart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_StudentTestAttempts_StudentTestAttemptId",
                table: "TestsQuestion");

            migrationBuilder.DropIndex(
                name: "IX_TestsQuestion_StudentTestAttemptId",
                table: "TestsQuestion");

            migrationBuilder.DropColumn(
                name: "StudentTestAttemptId",
                table: "TestsQuestion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentTestAttemptId",
                table: "TestsQuestion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestsQuestion_StudentTestAttemptId",
                table: "TestsQuestion",
                column: "StudentTestAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_StudentTestAttempts_StudentTestAttemptId",
                table: "TestsQuestion",
                column: "StudentTestAttemptId",
                principalTable: "StudentTestAttempts",
                principalColumn: "Id");
        }
    }
}
