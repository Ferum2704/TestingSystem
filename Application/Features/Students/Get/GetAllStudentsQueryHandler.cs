using Application.Abstractions;
using Application.Utilities;
using Application.ViewModels;
using AutoMapper;
using MediatR;

namespace Application.Features.Students.Get
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IReadOnlyCollection<StudentShortInfoViewModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllStudentsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<StudentShortInfoViewModel>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var students = await unitOfWork.StudentRepository.GetAsync();

            return mapper.Map<IReadOnlyCollection<StudentShortInfoViewModel>>(students);
        }
    }
}
