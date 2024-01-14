using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveTestStateToStudentAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Test");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "StudentTestAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "StudentTestAttempts");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
