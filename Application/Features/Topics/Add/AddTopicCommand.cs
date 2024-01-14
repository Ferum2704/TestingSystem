using Application.DTOs;
using MediatR;

namespace Application.Features.Topics.Add
{
    public class AddTopicCommand : IRequest<TopicDTO>
    {
        public string Title { get; set; }

        public Guid SubjectId { get; set; }
    }
}
