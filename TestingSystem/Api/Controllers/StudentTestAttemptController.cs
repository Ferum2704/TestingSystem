using Application.Features.StudentTestAttempts.Add;
using Application.Identitity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics/{topicId}/tests/{testId}/attempts")]
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
            Guid testId,
            StudentTestAttemptPostModel studentTestAttemptPostModel)
        {
            var createdAttemptsIds = await mediator.Send(new AddStudentTestAttemptCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentTestAttemptPostModel.StudentId,
                TestId = testId,
            });

            return Ok(createdAttemptsIds);
        }
    }
}
