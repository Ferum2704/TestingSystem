using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(e => e.Questions)
                .WithMany(e => e.Tests)
                .UsingEntity<TestQuestion>(
                    l => l.HasOne(x => x.Question).WithMany(x => x.TestQuestions).HasForeignKey(x => x.TestId),
                    r => r.HasOne(x => x.Test).WithMany(x => x.TestQuestions).HasForeignKey(x => x.QuestionId));
        }
    }
}
