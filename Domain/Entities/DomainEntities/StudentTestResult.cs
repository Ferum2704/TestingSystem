using Domain.Enums;

namespace Domain.Entities.DomainEntities
{
    public class StudentTestResult
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid TestQuestionId { get; set; }

        public TestQuestion TestQuestion { get; set; }

        public AnswerOption Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
