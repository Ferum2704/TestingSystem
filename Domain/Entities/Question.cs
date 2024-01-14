using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public AnswerOption CorrectAnswer { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<TestQuestion> TestQuestions { get; set; }

        public ICollection<StudentTestResult> Results { get; set; }
    }
}
