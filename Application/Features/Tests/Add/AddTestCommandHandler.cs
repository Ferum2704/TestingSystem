using Application.Abstractions;
using Application.DTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tests.Add
{
    public class AddTestCommandHandler : IRequestHandler<AddTestCommand, TestDTO>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TestDTO> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var test = mapper.Map<Test>(request);

            unitOfWork.TestRepository.Add(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<TestDTO>(test);
        }
    }
}
