using Application.DTOs.Enums;
using Domain.Enums;
using MediatR;

namespace Application.Features.StudentTestAttempts.Edit
{
    public class EditStudentTestAttemptCommand : IRequest
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }

        public Guid StudentId { get; set; }

        public TestStateDTO State { get; set; }
    }
}
