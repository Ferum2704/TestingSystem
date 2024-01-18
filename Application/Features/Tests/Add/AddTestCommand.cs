using Application.DTOs;
using MediatR;

namespace Application.Features.Tests.Add
{
    public class AddTestCommand : IRequest<TestDTO>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }

        public DateTime TestDate { get; set; }

        public string Name { get; set; }
    }
}
