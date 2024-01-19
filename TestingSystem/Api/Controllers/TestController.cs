using Application.Features.Tests.Add;
using Application.Features.Tests.Get;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/subjects/{subjectId}/topics/{topicId}")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public TestController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost("tests")]
        public async Task<IActionResult> PostTest(Guid topicId, TestModel testModel)
        {
            var addTestCommand = mapper.Map<AddTestCommand>(testModel);
            addTestCommand.TopicId = topicId;

            var createdTest = await mediator.Send(addTestCommand);

            return Ok(createdTest);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpGet("students/{studentId}/tests/{testId}")]
        public async Task<IActionResult> GetStudentTestDetails(Guid studentId, Guid testId)
        {
            var testDetails = await mediator.Send(new GetStudentTestDetailsQuery { StudentId = studentId, TestId = testId});

            return Ok(testDetails);
        }
    }
}
