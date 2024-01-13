using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StudentTestResultConfiguration : IEntityTypeConfiguration<StudentTestResult>
    {
        public void Configure(EntityTypeBuilder<StudentTestResult> builder)
        {
            builder.ToTable(nameof(StudentTestResult));

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.StudentAttempt)
                   .WithMany(x => x.Results)
                   .HasForeignKey(x => x.StudentAttemptId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

            builder.HasOne(x => x.Question)
                   .WithMany(x => x.Results)
                   .HasForeignKey(x => x.QuestionId)
                   .IsRequired();

            builder
                .Property(x => x.Answer)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
