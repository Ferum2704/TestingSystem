using Application.Features.StudentTestAttempts.Edit;
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
    [Route("api/subjects/{subjectId}/topics/{topicId}/tests")]
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
        [HttpPost]
        public async Task<IActionResult> PostTest(Guid topicId, TestModel testModel)
        {
            var addTestCommand = mapper.Map<AddTestCommand>(testModel);
            addTestCommand.TopicId = topicId;

            var createdTest = await mediator.Send(addTestCommand);

            return Ok(createdTest);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpGet("{testId}/student")]
        public async Task<IActionResult> GetStudentTestDetails(
            Guid subjectId,
            Guid topicId,
            Guid testId)
        {
            var testDetails = await mediator.Send(new GetStudentTestDetailsQuery { SubjectId = subjectId, TopicId = topicId, TestId = testId });

            return Ok(testDetails);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpGet("{testId}/teacher")]
        public async Task<IActionResult> GetTeacherTestDetails(
            Guid subjectId,
            Guid topicId,
            Guid testId)
        {
            var testDetails = await mediator.Send(new GetTeacherTestDetailsQuery { SubjectId = subjectId, TopicId = topicId, TestId = testId });

            return Ok(testDetails);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpPut("{testId}/students/{studentId}")]
        public async Task<IActionResult> PutTest(
            Guid subjectId,
            Guid topicId,
            Guid testId,
            Guid studentId,
            StudentTestAttemptPutModel studentTestAttemptModel)
        {
            await mediator.Send(new EditStudentTestAttemptCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
                State = studentTestAttemptModel.State,
            });

            return Ok();
        }
    }
}
