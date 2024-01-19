using Application.Abstractions;
using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.StudentTestResults.Put
{
    public class PutStudentTestResultCommandHandler : IRequestHandler<PutStudentTestResultCommand, StudentTestResultDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PutStudentTestResultCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<StudentTestResultDTO> Handle(PutStudentTestResultCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var currentTestAttempt = await GetCurrentAttempt(request.TestId, request.StudentId);

            if (currentTestAttempt != null)
            {
                var studentResult = (await unitOfWork.StudentTestResultRepository
                    .GetAsync(x => x.StudentAttemptId == currentTestAttempt.Id && x.QuestionId == request.QuestionId, cancellationToken)).FirstOrDefault();
                var question = await unitOfWork.QuestionRepository.GetByIdAsync(request.QuestionId);

                if (studentResult is null)
                {
                    var newStudentResult = mapper.Map<StudentTestResult>(request);
                    newStudentResult.IsCorrect = question.CorrectAnswer == newStudentResult.Answer;
                    newStudentResult.StudentAttemptId = currentTestAttempt.Id;

                    unitOfWork.StudentTestResultRepository.Add(newStudentResult);
                    await unitOfWork.SaveAsync();

                    return mapper.Map<StudentTestResultDTO>(newStudentResult);
                }
                else
                {
                    studentResult.Answer = (AnswerOption)request.Answer;
                    studentResult.IsCorrect = question.CorrectAnswer == studentResult.Answer;

                    unitOfWork.StudentTestResultRepository.Update(studentResult);
                    await unitOfWork.SaveAsync();

                    return mapper.Map<StudentTestResultDTO>(studentResult);
                }
            }

            return new StudentTestResultDTO();
        }

        private async Task<StudentTestAttempt> GetCurrentAttempt(Guid testId, Guid studentId) =>
            (await unitOfWork.StudentTestAttemptRepository.GetAsync(x => x.StudentId == studentId && x.TestId == testId && x.State == TestState.InProgress)).SingleOrDefault();
    }
}
