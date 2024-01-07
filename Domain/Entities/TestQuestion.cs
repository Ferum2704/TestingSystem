using Domain.Interfaces;

namespace Domain.Entities
{
    public class TestQuestion : IEntity
    {
        public Guid Id { get; set; }

        public Guid TestId { get; set; }

        public Test Test { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public ICollection<StudentTestResult> Results { get; set; }
    }
}
