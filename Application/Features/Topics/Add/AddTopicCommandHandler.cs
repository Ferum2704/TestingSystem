using Application.Abstractions;
using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Topics.Add
{
    public class AddTopicCommandHandler : IRequestHandler<AddTopicCommand, TopicDTO>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddTopicCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TopicDTO> Handle(AddTopicCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var topic = mapper.Map<Topic>(request);

            unitOfWork.TopicRepository.Add(topic);
            await unitOfWork.SaveAsync();

            return mapper.Map<TopicDTO>(topic);
        }
    }
}
