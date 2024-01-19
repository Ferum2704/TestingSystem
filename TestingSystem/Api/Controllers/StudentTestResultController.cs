using Application.Features.StudentTestResults.Put;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics/{topicId}/students/{studentId}/tests/{testId}/questions/{questionId}attempts/{attemptId}")]
    [ApiController]
    public class StudentTestResultController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentTestResultController(IMapper mapper, IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> PutStudentTestResult(
            Guid subjectId,
            Guid topicId,
            Guid studentId,
            Guid testId,
            Guid questionId,
            Guid attemptId,
            StudentTestResultModel studentTestResultModel)
        {
            var studentTestResult = await mediator.Send(new PutStudentTestResultCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
                QuestionId = questionId,
                AttemptId = attemptId,
                Answer = studentTestResultModel.Answer,
            });

            return Ok(studentTestResult);
        }
    }
}
