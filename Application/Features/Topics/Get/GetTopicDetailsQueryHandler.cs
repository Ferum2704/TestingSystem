using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Topics.Get
{
    public class GetTopicDetailsQueryHandler : IRequestHandler<GetTopicDetailsQuery, TopicViewModel>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetTopicDetailsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TopicViewModel> Handle(GetTopicDetailsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var topic = await unitOfWork.TopicRepository.GetByIdAsync(request.TopicId);

            return mapper.Map<TopicViewModel>(topic);
        }
    }
}
