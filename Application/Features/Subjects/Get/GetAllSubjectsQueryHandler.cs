using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels.SubjectVMs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Subjects.Get
{
    internal class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, IReadOnlyCollection<SubjectViewModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllSubjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SubjectViewModel>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var allSubjects = await unitOfWork.SubjectRepository.GetAsync();

            return mapper.Map<IReadOnlyCollection<SubjectViewModel>>(allSubjects);
        }
    }
}
