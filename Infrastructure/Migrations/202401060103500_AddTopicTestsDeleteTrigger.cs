using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("AddTopicQuestionsDeleteTrigger")]
    public partial class _20231226081400_AddTopicQuestionsDeleteTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE TRIGGER trg_DeleteQuestionsOnTopicDelete
                    ON Topic
                    AFTER DELETE
                    AS
                    BEGIN
                        SET NOCOUNT ON;

                        DELETE FROM Question
                        WHERE TopicId IN (SELECT deleted.Id FROM deleted);
                    END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP TRIGGER trg_DeleteQuestionsOnTopicDelete;");
        }
    }
}