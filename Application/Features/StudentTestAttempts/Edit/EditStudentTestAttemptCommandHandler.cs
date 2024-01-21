using Application.Abstractions;
using Application.DTOs.Enums;
using Application.Utilities;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.StudentTestAttempts.Edit
{
    public class EditStudentTestAttemptCommandHandler : IRequestHandler<EditStudentTestAttemptCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public EditStudentTestAttemptCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(EditStudentTestAttemptCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var testAttempt = request.State switch
            {
                TestStateDTO.InProgress => await StartTest(request, cancellationToken),
                TestStateDTO.Finished => await FinishTest(request, cancellationToken),
                _ => throw new ArgumentException()
            };

            unitOfWork.StudentTestAttemptRepository.Update(testAttempt);

            await unitOfWork.SaveAsync();
        }

        private async Task<StudentTestAttempt> StartTest(EditStudentTestAttemptCommand request, CancellationToken cancellationToken)
        {
            var testAttempt = (await unitOfWork.StudentTestAttemptRepository
                .GetAsync(x => x.TestId == request.TestId && x.StudentId == request.StudentId && x.State == TestState.NotStarted, cancellationToken))
                .OrderBy(x => x.NumberOfAttemt)
                .FirstOrDefault();

            testAttempt.State = TestState.InProgress;
            testAttempt.StartedAt = DateTime.UtcNow;

            return testAttempt;
        }

        private async Task<StudentTestAttempt> FinishTest(EditStudentTestAttemptCommand request, CancellationToken cancellationToken)
        {
            var testAttempt = (await unitOfWork.StudentTestAttemptRepository
                .GetAsync(x => x.TestId == request.TestId && x.StudentId == request.StudentId && x.State == TestState.InProgress, cancellationToken))
                .SingleOrDefault();
            await CreateNotAnsweredResults(testAttempt);

            testAttempt.State = TestState.Finished;
            testAttempt.FinishedAt = DateTime.UtcNow;

            return testAttempt;
        }

        private async Task CreateNotAnsweredResults(StudentTestAttempt studentTestAttempt)
        {
            var answeredQuestionsIds = studentTestAttempt.Results.Select(x => x.QuestionId).ToList();

            var notAnsweredQuestionsIds = (await unitOfWork.QuestionRepository.GetAsync(x => !answeredQuestionsIds.Contains(x.Id))).Select(x => x.Id);

            foreach (var id in notAnsweredQuestionsIds)
            {
                var newResult = new StudentTestResult
                {
                    Id = Guid.NewGuid(),
                    StudentAttemptId = studentTestAttempt.Id,
                    QuestionId = id,
                };

                unitOfWork.StudentTestResultRepository.Add(newResult);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
