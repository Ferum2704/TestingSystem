using Application.Abstractions;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.StudentTestAttempts.Add
{
    public class AddStudentTestAttemptCommandHandler : IRequestHandler<AddStudentTestAttemptCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddStudentTestAttemptCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddStudentTestAttemptCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var testAttempts = await unitOfWork.StudentTestAttemptRepository.GetAsync(x => x.StudentId == request.StudentId && x.TestId == request.TestId, cancellationToken);

            var studentAttempt = mapper.Map<StudentTestAttempt>(request);

            if (testAttempts.Count == 0)
            {
                studentAttempt.NumberOfAttemt = 1;
            }
            else
            {
                studentAttempt.NumberOfAttemt = ++testAttempts.MaxBy(x => x.NumberOfAttemt).NumberOfAttemt;
            }

            unitOfWork.StudentTestAttemptRepository.Add(studentAttempt);
            await unitOfWork.SaveAsync();

            return studentAttempt.Id;
        }
    }
}
