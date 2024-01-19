using Application.Features.StudentTestAttempts.Add;
using Application.Features.StudentTestAttempts.Edit;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics/{topicId}/students/{studentId}/tests/{testId}/attempts")]
    [ApiController]
    public class StudentTestAttemptController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentTestAttemptController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost]
        public async Task<IActionResult> PostStudentTestAttempt(
            Guid subjectId,
            Guid topicId,
            Guid studentId,
            Guid testId)
        {
            var createdAttemptsIds = await mediator.Send(new AddStudentTestAttemptCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
            });

            return Ok(createdAttemptsIds);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpPut]
        public async Task<IActionResult> PutStudentTestAttempt(
            Guid subjectId,
            Guid topicId,
            Guid studentId,
            Guid testId,
            Guid attemptId,
            StudentTestAttemptModel studentTestAttemptModel)
        {
            await mediator.Send(new EditStudentTestAttemptCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
                AttemptId = attemptId,
                State = studentTestAttemptModel.State,
            });

            return Ok();
        }
    }
}
