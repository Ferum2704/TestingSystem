using Application.Features.Students.Get;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentController(IMapper mapper, IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await mediator.Send(new GetAllStudentsQuery());

            return Ok(students);
        }
    }
}
