using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class StudentTestResult : IEntity
    {
        public Guid Id { get; set; }

        public Guid StudentAttemptId { get; set; }

        public StudentTestAttempt StudentAttempt { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public AnswerOption Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
