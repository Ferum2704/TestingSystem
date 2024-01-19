using Application.Abstractions;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Add
{
    public class AddQuestionToTestCommandHandler : IRequestHandler<AddQuestionToTestCommand>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddQuestionToTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(AddQuestionToTestCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var testQuestion = mapper.Map<TestQuestion>(request);

            unitOfWork.QuestionRepository.AddQuestionToTest(testQuestion);

            await unitOfWork.SaveAsync();
        }
    }
}
