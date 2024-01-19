using Application.Abstractions;
using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
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

            var studentResult = (await unitOfWork.StudentTestResultRepository
                .GetAsync(x => x.StudentAttempt.Id == request.AttemptId && x.QuestionId == request.QuestionId, cancellationToken)).FirstOrDefault();
            var question = await unitOfWork.QuestionRepository.GetByIdAsync(request.QuestionId);

            if (studentResult is null)
            {
                var newStudentResult = mapper.Map<StudentTestResult>(request);
                newStudentResult.IsCorrect = question.CorrectAnswer == newStudentResult.Answer;

                unitOfWork.StudentTestResultRepository.Add(newStudentResult);
                await unitOfWork.SaveAsync();

                return mapper.Map<StudentTestResultDTO>(newStudentResult);
            }
            else
            {
                studentResult.Answer = request.Answer;
                studentResult.IsCorrect = question.CorrectAnswer == studentResult.Answer;

                unitOfWork.StudentTestResultRepository.Update(studentResult);
                await unitOfWork.SaveAsync();

                return mapper.Map<StudentTestResultDTO>(studentResult);
            }
        }
    }
}
