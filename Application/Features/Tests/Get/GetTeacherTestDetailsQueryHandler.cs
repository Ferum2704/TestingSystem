using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels;
using Application.ViewModels.StudentVMs;
using Application.ViewModels.TestVMs;
using AutoMapper;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetTeacherTestDetailsQueryHandler : IRequestHandler<GetTeacherTestDetailsQuery, TeacherTestDetailsViewModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTeacherTestDetailsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TeacherTestDetailsViewModel> Handle(GetTeacherTestDetailsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var testAttempts = await unitOfWork.StudentTestAttemptRepository.GetAsync(x => x.TestId == request.TestId, cancellationToken);

            var groupedTestDetailsByStudent = testAttempts.GroupBy(x => x.StudentId).Select(grp => new StudentTestInfoViewModel
            {
                Id = grp.Key,
                Attempts = mapper.Map<IReadOnlyCollection<StudentTestAttemptViewModel>>(grp.ToList()),
            }).ToList();

            return new TeacherTestDetailsViewModel { StudentResults = groupedTestDetailsByStudent };
        }
    }
}
