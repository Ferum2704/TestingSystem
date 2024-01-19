using Domain.Enums;

namespace Application.ViewModels.QuestionVMs
{
    public class TopicQuestionViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public AnswerOption CorrectAnswer { get; set; }
    }
}
