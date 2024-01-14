using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Subjects.Add
{
    public class AddSubjectCommandHandler : IRequestHandler<AddSubjectCommand, SubjectDTO>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddSubjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<SubjectDTO> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var subject = mapper.Map<Subject>(request);

            unitOfWork.SubjectRepository.Add(subject);
            await unitOfWork.SaveAsync();

            return mapper.Map<SubjectDTO>(subject);
        }
    }
}
