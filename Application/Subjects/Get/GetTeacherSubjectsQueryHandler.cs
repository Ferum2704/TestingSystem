using Application.Utilities;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Subjects.GetSubjects
{
    public class GetTeacherSubjectsQueryHandler : IRequestHandler<GetTeacherSubjectsQuery, IReadOnlyCollection<SubjectViewModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTeacherSubjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SubjectViewModel>> Handle(GetTeacherSubjectsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var teacherSubjects = await unitOfWork.SubjectRepository.Find(x => x.TeacherId == request.TeacherId, new List<string> { $"{nameof(Topic)}s" });

            return mapper.Map<IReadOnlyCollection<SubjectViewModel>>(teacherSubjects);
        }
    }
}
