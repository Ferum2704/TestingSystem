using Application.Abstractions;
using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Add
{
    public class AddQuestionToTopicCommandHandler : IRequestHandler<AddQuestionToTopicCommand, QuestionDTO>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuestionsFileService questionsFileService;

        public AddQuestionToTopicCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IQuestionsFileService questionsFileService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.questionsFileService = questionsFileService;
        }

        public async Task<QuestionDTO> Handle(AddQuestionToTopicCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var question = mapper.Map<Question>(request);
            var topic = await unitOfWork.TopicRepository.GetByIdAsync(request.TopicId);

            questionsFileService.WriteTopicQuestion(topic.Title, question);

            unitOfWork.QuestionRepository.Add(question);
            await unitOfWork.SaveAsync();

            return mapper.Map<QuestionDTO>(question);
        }
    }
}
