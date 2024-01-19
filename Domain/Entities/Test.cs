using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Test : IEntity
    {
        public Guid Id { get; set; }

        public DateTime TestDate { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public int NumberOfAttempts { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public IReadOnlyCollection<Question> Questions { get; set; }

        public IReadOnlyCollection<TestQuestion> TestQuestions { get; set; }

        public IReadOnlyCollection<Student> Students { get; set; }

        public IReadOnlyCollection<StudentTestAttempt> StudentsAttempts { get; set; }
    }
}
