using Application.ViewModels.TestVMs;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetStudentTestDetailsQuery : IRequest<StudentTestDetailsViewModel>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }
    }
}
