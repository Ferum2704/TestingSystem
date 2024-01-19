using Application.ViewModels.SubjectVMs;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetAllSubjectsQuery : IRequest<IReadOnlyCollection<SubjectViewModel>>
    {
    }
}
