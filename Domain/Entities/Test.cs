using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Test : IEntity
    {
        public Guid Id { get; set; }

        public DateTime TestDate { get; set; }

        public string Name { get; set; }

        public Guid StudentId { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<TestQuestion> TestQuestions { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<StudentTestAttempt> StudentsAttempts { get; set; }
    }
}
