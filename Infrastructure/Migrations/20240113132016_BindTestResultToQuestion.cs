using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BindTestResultToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestResult_TestsQuestion_TestQuestionId",
                table: "StudentTestResult");

            migrationBuilder.DropColumn(
                name: "OptionA",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "OptionB",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "OptionC",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "TestQuestionId",
                table: "StudentTestResult",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTestResult_TestQuestionId",
                table: "StudentTestResult",
                newName: "IX_StudentTestResult_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestResult_Question_QuestionId",
                table: "StudentTestResult",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestResult_Question_QuestionId",
                table: "StudentTestResult");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "StudentTestResult",
                newName: "TestQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentTestResult_QuestionId",
                table: "StudentTestResult",
                newName: "IX_StudentTestResult_TestQuestionId");

            migrationBuilder.AddColumn<string>(
                name: "OptionA",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OptionB",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OptionC",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestResult_TestsQuestion_TestQuestionId",
                table: "StudentTestResult",
                column: "TestQuestionId",
                principalTable: "TestsQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
