using Application.ViewModels;
using MediatR;

namespace Application.Subjects.GetSubjects
{
    public class GetTeacherSubjectsQuery : IRequest<IReadOnlyCollection<SubjectViewModel>>
    {
        public Guid TeacherId { get; set; }
    }
}
