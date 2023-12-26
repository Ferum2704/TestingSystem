using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Subjects)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId)
                .IsRequired();
        }
    }
}
