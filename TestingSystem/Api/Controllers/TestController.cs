using Application.Abstractions;
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
        private readonly ICurrentUserService currentUserService;

        public TestController(IMapper mapper, IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.currentUserService = currentUserService;
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

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}, {nameof(ApplicationUserRole.Teacher)}")]
        [HttpGet("{testId}")]
        public async Task<IActionResult> GetStudentTestDetails(
            Guid subjectId,
            Guid topicId,
            Guid testId)
        {
            if (currentUserService.IsInRole(ApplicationUserRole.Student))
            {
                var testDetails = await mediator.Send(new GetStudentTestDetailsQuery { SubjectId = subjectId, TopicId = topicId, TestId = testId });

                return Ok(testDetails);
            }
            else if (currentUserService.IsInRole(ApplicationUserRole.Teacher))
            {
                var testDetails = await mediator.Send(new GetTeacherTestDetailsQuery { SubjectId = subjectId, TopicId = topicId, TestId = testId });

                return Ok(testDetails);
            }

            return Ok();
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
