﻿using Application.ViewModels.TestVMs;
using MediatR;

namespace Application.Features.Tests.Get
{
    public class GetStudentTestDetailsQuery : IRequest<StudentTestDetailsViewModel>
    {
        public Guid TestId { get; set; }
    }
}