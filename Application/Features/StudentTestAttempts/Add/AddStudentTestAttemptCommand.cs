using Domain.Enums;
using MediatR;

namespace Application.Features.StudentTestAttempts.Add
{
    public class AddStudentTestAttemptCommand : IRequest<Guid>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }

        public Guid StudentId { get; set; }
    }
}
