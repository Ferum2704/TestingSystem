using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Topics)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId)
                .IsRequired();
                
        }
    }
}
