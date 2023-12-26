using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Tests)
                .WithOne(x => x.Topic)
                .HasForeignKey(x => x.TopicId)
                .IsRequired();

            builder
                .HasMany(x => x.Questions)
                .WithOne(x => x.Topic)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
