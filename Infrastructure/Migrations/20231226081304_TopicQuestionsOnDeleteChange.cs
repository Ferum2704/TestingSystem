using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TopicQuestionsOnDeleteChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Questions_TestId",
                table: "TestsQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Tests_QuestionId",
                table: "TestsQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Questions_TestId",
                table: "TestsQuestion",
                column: "TestId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Tests_QuestionId",
                table: "TestsQuestion",
                column: "QuestionId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Questions_TestId",
                table: "TestsQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsQuestion_Tests_QuestionId",
                table: "TestsQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Questions_TestId",
                table: "TestsQuestion",
                column: "TestId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsQuestion_Tests_QuestionId",
                table: "TestsQuestion",
                column: "QuestionId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
