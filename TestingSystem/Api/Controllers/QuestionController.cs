using Application.Features.Questions.Add;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/subjects/{subjectId}/topics{topicId}")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public QuestionController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost]
        public async Task<IActionResult> PostQuestionToTopic(
            Guid subjectId,
            Guid topicId,
            QuestionModel questionModel)
        {
            var addQuestionCommand = mapper.Map<AddQuestionToTopicCommand>(questionModel);
            addQuestionCommand.SubjectId = subjectId;
            addQuestionCommand.TopicId = topicId;

            var createdQuestion = await mediator.Send(addQuestionCommand);

            return Ok(createdQuestion);
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost("tests/{testId}")]
        public async Task<IActionResult> PostQuestionToTest(
            Guid subjectId,
            Guid topicId,
            Guid testId,
            TestQuestionModel testQuestionModel)
        {
            var addTestQuestionuestionCommand = mapper.Map<AddQuestionToTestCommand>(testQuestionModel);
            addTestQuestionuestionCommand.SubjectId = subjectId;
            addTestQuestionuestionCommand.TopicId = topicId;
            addTestQuestionuestionCommand.TestId = testId;

            await mediator.Send(addTestQuestionuestionCommand);

            return Ok();
        }
    }
}
