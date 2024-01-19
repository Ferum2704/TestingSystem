using Application.ViewModels;
using MediatR;

namespace Application.Features.Students.Get
{
    public class GetAllStudentsQuery : IRequest<IReadOnlyCollection<StudentShortInfoViewModel>>
    {
    }
}
