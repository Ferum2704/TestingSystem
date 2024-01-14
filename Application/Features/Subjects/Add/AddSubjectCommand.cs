using Application.DTOs;
using MediatR;

namespace Application.Features.Subjects.Add
{
    public class AddSubjectCommand : IRequest<SubjectDTO>
    {
        public string Name { get; set; }

        public Guid TeacherId { get; set; }
    }
}
