using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class StudentTestAttemptConfiguration : IEntityTypeConfiguration<StudentTestAttempt>
    {
        public void Configure(EntityTypeBuilder<StudentTestAttempt> builder)
        {
            builder.HasKey(t => t.Id);
            builder.
        }
    }
}
