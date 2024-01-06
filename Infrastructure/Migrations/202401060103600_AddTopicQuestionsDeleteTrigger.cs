using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("AddTopicTestsDeleteTrigger")]
    public partial class _20240106104000_AddTopicTestsDeleteTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE TRIGGER trg_DeleteTestsOnTopicDelete
                    ON Topic
                    AFTER DELETE
                    AS
                    BEGIN
                        SET NOCOUNT ON;

                        DELETE FROM Test
                        WHERE TopicId IN (SELECT deleted.Id FROM deleted);
                    END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP TRIGGER trg_DeleteTestsOnTopicDelete;");
        }
    }
}