using Application.Identitity;
using Application.Subjects.GetSubjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/teachers")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator mediator;

        public TeacherController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpGet("{teacherId}/subjects")]
        public async Task<IActionResult> GetTeacherSubjects(Guid teacherId)
        {
            var teacherSubjects = await mediator.Send(new GetTeacherSubjectsQuery { TeacherId = teacherId });

            return Ok(teacherSubjects);
        }
    }
}
