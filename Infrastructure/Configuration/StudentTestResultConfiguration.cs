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

            builder.HasOne(x => x.Student)
                   .WithMany(x => x.TestResults) 
                   .HasForeignKey(x => x.StudentId)
                   .IsRequired();

            builder.HasOne(x => x.TestQuestion)
                   .WithMany(x => x.Results)
                   .HasForeignKey(x => x.TestQuestionId)
                   .IsRequired();

            builder
                .Property(x => x.Answer)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
