using Application.Features.Topics.Add;
using Application.Identitity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/subjects/{subjectId}/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public TopicController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [Authorize(Roles = $"{nameof(ApplicationUserRole.Teacher)}")]
        [HttpPost]
        public async Task<IActionResult> PostTopic(Guid subjectId, TopicModel topicModel)
        {
            var addTopicCommand = mapper.Map<AddTopicCommand>(topicModel);
            addTopicCommand.SubjectId = subjectId;

            var createdTopic = await mediator.Send(addTopicCommand);

            return Ok(createdTopic);
        }
    }
}
