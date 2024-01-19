using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SwapForeignKeysInTestQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Question_TestId",
                table: "TestsQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Test_QuestionId",
                table: "TestsQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Question_QuestionId",
                table: "TestsQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Test_TestId",
                table: "TestsQuestion",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Question_QuestionId",
                table: "TestsQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Test_TestId",
                table: "TestsQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Question_TestId",
                table: "TestsQuestion",
                column: "TestId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Test_QuestionId",
                table: "TestsQuestion",
                column: "QuestionId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
