using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetTeacherSubjectsQueryHandler : IRequestHandler<GetTeacherSubjectsQuery, IReadOnlyCollection<SubjectInfoViewModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTeacherSubjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SubjectInfoViewModel>> Handle(GetTeacherSubjectsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var teacherSubjects = await unitOfWork.SubjectRepository.GetAsync(x => x.TeacherId == request.TeacherId);

            return mapper.Map<IReadOnlyCollection<SubjectInfoViewModel>>(teacherSubjects);
        }
    }
}
