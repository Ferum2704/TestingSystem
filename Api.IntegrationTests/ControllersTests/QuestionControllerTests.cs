using Api.IntegrationTests.AutoFixture;
using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Models;
using Xunit;

namespace Api.IntegrationTests.ControllersTests
{
    [Collection("SharedWebFactory")]
    public class QuestionControllerTests : TestApiBase
    {
        public QuestionControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) : base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task PostQuestion_ShouldCreateQuestionToTopic(
            Subject subject,
            Topic topicEntity,
            QuestionModel questionModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            await PrepareTestData(scope, topicEntity, subject);
            var formattedUrl = string.Format(ApiUrls.PostQuestion, subject.Id, topicEntity.Id);

            var createdQuestion = await PostAsync<QuestionDTO, QuestionModel>(formattedUrl, questionModel, TeacherTestUsername, TeacherTestUsernamePassword);

            createdQuestion.Should().NotBeNull();
            createdQuestion.CorrectAnswer.Should().Be(questionModel.CorrectAnswer);
            createdQuestion.OptionA.Should().Be(questionModel.OptionA);
            createdQuestion.OptionB.Should().Be(questionModel.OptionB);
            createdQuestion.OptionC.Should().Be(questionModel.OptionC);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var topic = await unitOfWork.QuestionRepository.GetByIdAsync(createdQuestion.Id);
            topic.Should().NotBeNull();
        }

        private async Task PrepareTestData(IServiceScope scope, Topic topic, Subject subject)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var domainUserId = await PrepareTestUsers(false);

            subject.TeacherId = domainUserId;
            topic.SubjectId = subject.Id;
            unitOfWork.SubjectRepository.Add(subject);
            unitOfWork.TopicRepository.Add(topic);
            await unitOfWork.SaveAsync();
        }
    }
}
