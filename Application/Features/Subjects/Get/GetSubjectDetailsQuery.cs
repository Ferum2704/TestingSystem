using Application.ViewModels;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetSubjectDetailsQuery : IRequest<SubjectViewModel>
    {
        public Guid Id { get; set; }
    }
}
