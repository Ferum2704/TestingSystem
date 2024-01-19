using Application.ViewModels.TestVMs;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetTeacherTestDetailsQuery : IRequest<TeacherTestDetailsViewModel>
    {
        public Guid TestId { get; set; }
    }
}
