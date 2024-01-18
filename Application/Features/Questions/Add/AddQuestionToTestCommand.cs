using Application.DTOs;
using MediatR;

namespace Application.Features.Questions.Add
{
    public class AddQuestionToTestCommand : IRequest
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }

        public Guid QuestionId { get; set; }
    }
}
