using Application.Features.StudentTestResults.Put;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics/{topicId}/students/{studentId}/tests/{testId}/questions/{questionId}")]
    [ApiController]
    public class StudentTestResultController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentTestResultController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Student)}")]
        [HttpPut]
        public async Task<IActionResult> PutStudentTestResult(
            Guid subjectId,
            Guid topicId,
            Guid studentId,
            Guid testId,
            Guid questionId,
            StudentTestResultModel studentTestResultModel)
        {
            var studentTestResult = await mediator.Send(new PutStudentTestResultCommand()
            {
                SubjectId = subjectId,
                TopicId = topicId,
                StudentId = studentId,
                TestId = testId,
                QuestionId = questionId,
                Answer = studentTestResultModel.Answer,
            });

            return Ok(studentTestResult);
        }
    }
}
