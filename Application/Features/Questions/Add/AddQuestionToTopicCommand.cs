using Application.DTOs;
using Application.DTOs.Enums;
using Domain.Enums;
using MediatR;

namespace Application.Features.Questions.Add
{
    public class AddQuestionToTopicCommand : IRequest<QuestionDTO>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public string Text { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public AnswerOptionDTO CorrectAnswer { get; set; }
    }
}
