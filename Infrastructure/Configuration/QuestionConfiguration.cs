using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question));

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.OptionA);
            builder.Ignore(x => x.OptionB);
            builder.Ignore(x => x.OptionC);

            builder
                .Property(x => x.CorrectAnswer)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
