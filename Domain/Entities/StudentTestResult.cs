using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class StudentTestResult : IEntity
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
