using Application.Abstractions;
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

            var testAttempt = await unitOfWork.StudentTestAttemptRepository.GetByIdAsync(request.AttemptId, cancellationToken);

            if (testAttempt is not null)
            {
                switch (request.State)
                {
                    case DTOs.Enums.TestStateDTO.InProgress:
                        testAttempt.State = TestState.InProgress;
                        testAttempt.StartedAt = DateTime.UtcNow;
                        break;
                    case DTOs.Enums.TestStateDTO.Finished:
                        CreateNotAnsweredResults(testAttempt);
                        testAttempt.State = TestState.Finished;
                        testAttempt.FinishedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }

                unitOfWork.StudentTestAttemptRepository.Update(testAttempt);

                await unitOfWork.SaveAsync();
            }
        }

        private async void CreateNotAnsweredResults(StudentTestAttempt studentTestAttempt)
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
            }
        }
    }
}
