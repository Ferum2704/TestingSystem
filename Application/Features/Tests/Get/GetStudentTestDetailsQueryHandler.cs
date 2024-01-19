using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels;
using Application.ViewModels.QuestionVMs;
using Application.ViewModels.TestVMs;
using AutoMapper;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetStudentTestDetailsQueryHandler : IRequestHandler<GetStudentTestDetailsQuery, StudentTestDetailsViewModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetStudentTestDetailsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<StudentTestDetailsViewModel> Handle(GetStudentTestDetailsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var testQuestions = (await unitOfWork.TestQuestionRepository.GetAsync(x => x.TestId == request.TestId, cancellationToken)).Select(x => x.Question).ToList();
            var testAttempts = await unitOfWork.StudentTestAttemptRepository.GetAsync(x => x.TestId == request.TestId, cancellationToken);

            var testDetails = new StudentTestDetailsViewModel
            {
                Questions = mapper.Map<IReadOnlyCollection<TestQuestionViewModel>>(testQuestions),
                Attempts = mapper.Map<IReadOnlyCollection<StudentTestAttemptViewModel>>(testAttempts),
            };

            return testDetails;
        }
    }
}
