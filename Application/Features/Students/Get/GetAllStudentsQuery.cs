using Application.ViewModels.StudentVMs;
using MediatR;

namespace Application.Features.Students.Get
{
    public class GetAllStudentsQuery : IRequest<IReadOnlyCollection<StudentShortInfoViewModel>>
    {
    }
}
