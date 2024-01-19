using Application.DTOs;
using Application.DTOs.Enums;
using Domain.Enums;
using MediatR;

namespace Application.Features.StudentTestResults.Put
{
    public class PutStudentTestResultCommand : IRequest<StudentTestResultDTO>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid StudentId { get; set; }

        public AnswerOptionDTO Answer { get; set; }
    }
}
