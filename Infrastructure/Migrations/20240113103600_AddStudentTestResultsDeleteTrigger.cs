using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("AddStudentTestResultsDeleteTrigger")]
    public class _20240113103600_AddStudentTestResultsDeleteTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE TRIGGER trg_DeleteStudentTestResultsOnAttemptDelete
                    ON StudentTestAttempts
                    AFTER DELETE
                    AS
                    BEGIN
                        SET NOCOUNT ON;

                        DELETE FROM StudentTestResult
                        WHERE StudentAttemptId IN (SELECT deleted.Id FROM deleted);
                    END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP TRIGGER trg_DeleteStudentTestResultsOnAttemptDelete;");
        }
    }
}
