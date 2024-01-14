using Application.ViewModels;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetTeacherSubjectsQuery : IRequest<IReadOnlyCollection<SubjectViewModel>>
    {
        public Guid TeacherId { get; set; }
    }
}
