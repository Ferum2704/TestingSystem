using Application.ViewModels;
using MediatR;

namespace Application.Features.Topics.Get
{
    public class GetTopicDetailsQuery : IRequest<TopicViewModel>
    {
        public Guid SubjectId { get; set; }

        public Guid TopicId { get; set; }
    }
}
