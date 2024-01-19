using Application.Abstractions;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.StudentTestAttempts.Add
{
    public class AddStudentTestAttemptCommandHandler : IRequestHandler<AddStudentTestAttemptCommand, IReadOnlyCollection<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddStudentTestAttemptCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Guid>> Handle(AddStudentTestAttemptCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var test = await unitOfWork.TestRepository.GetByIdAsync(request.TestId, cancellationToken);
            var createdAttemptsIds = new List<Guid>();

            for (int i = 1; i < test.NumberOfAttempts; i++)
            {
                var studentAttempt = mapper.Map<StudentTestAttempt>(request);
                studentAttempt.NumberOfAttemt = i;
                unitOfWork.StudentTestAttemptRepository.Add(studentAttempt);
                createdAttemptsIds.Add(studentAttempt.Id);
            }

            await unitOfWork.SaveAsync();

            return createdAttemptsIds;
        }
    }
}
