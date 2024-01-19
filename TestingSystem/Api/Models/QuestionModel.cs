using Application.DTOs.Enums;
using Domain.Enums;

namespace Presentation.Api.Models
{
    public class QuestionModel
    {
        public string Text { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public AnswerOptionDTO CorrectAnswer { get; set; }
    }
}
