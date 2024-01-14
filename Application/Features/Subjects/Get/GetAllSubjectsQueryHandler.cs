﻿using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
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
            var allSubjects = await unitOfWork.SubjectRepository.GetAll([$"{nameof(Topic)}s"]);

            return mapper.Map<IReadOnlyCollection<SubjectViewModel>>(allSubjects);
        }
    }
}
