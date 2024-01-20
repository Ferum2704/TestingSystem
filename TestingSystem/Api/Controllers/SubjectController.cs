using Application.Features.Subjects.Add;
using Application.Features.Subjects.Get;
using Application.Identitity;
using AutoMapper;
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

            return CreatedAtRoute(nameof(GetSubjectDetails), new { id = createdSubject.Id }, createdSubject);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var allSubjects = await mediator.Send(new GetAllSubjectsQuery());

            return Ok(allSubjects);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}, {nameof(ApplicationUserRole.Teacher)}")]
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetSubjectDetails(Guid subjectId)
        {
            var subjectDetails = await mediator.Send(new GetSubjectDetailsQuery { Id = subjectId });

            if (subjectDetails is null)
            {
                return NotFound();
            }

            return Ok(subjectDetails);
        }
    }
}
