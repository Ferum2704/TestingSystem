using Application.ViewModels;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetAllSubjectsQuery : IRequest<IReadOnlyCollection<SubjectViewModel>>
    {
    }
}
