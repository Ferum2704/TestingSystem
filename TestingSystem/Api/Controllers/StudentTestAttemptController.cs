using Application.Features.StudentTestAttempts.Add;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics/{topicId}/students/{studentId}/tests/{testId}/attempts")]
    [ApiController]
    public class StudentTestAttemptController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public StudentTestAttemptController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostStudentTestAttempt(
            Guid subjectId,
            Guid topicId,
            Guid studentId,
            Guid testId)
        {
            var createdAttemptId = await mediator.Send(new AddStudentTestAttemptCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
            });

            return Ok(createdAttemptId);
        }
    }
}
