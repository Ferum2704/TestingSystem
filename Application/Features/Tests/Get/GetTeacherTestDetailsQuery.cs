using Application.ViewModels.TestVMs;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetTeacherTestDetailsQuery : IRequest<TeacherTestDetailsViewModel>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public Guid TestId { get; set; }
    }
}
