using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));

            builder
                .HasMany(x => x.Tests)
                .WithMany(x => x.Students)
                .UsingEntity<StudentTestAttempt>();

            builder
                .Property(x => x.NumberOfAttemts)
                .HasDefaultValue(2);
        }
    }
}
