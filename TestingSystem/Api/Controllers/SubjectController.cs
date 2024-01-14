using Application.DTOs;
using Application.Identitity;
using Application.Subjects.Add;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;
using System.Security.Claims;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public SubjectController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost]
        public async Task<IActionResult> PostSubject(SubjectModel subjectModel)
        {
            var addSubjectCommand = mapper.Map<AddSubjectCommand>(subjectModel);
            addSubjectCommand.TeacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var createdSubject = await mediator.Send(addSubjectCommand);

            return Ok(createdSubject);
        }
    }
}
