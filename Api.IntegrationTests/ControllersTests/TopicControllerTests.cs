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
    public class TopicControllerTests : TestApiBase
    {
        public TopicControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) :
            base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task PostTopic_ShouldCreateTopicSubject(
            Subject subject,
            TopicModel topicModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            await PrepareTestData(scope, subject);
            var formattedUrl = string.Format(ApiUrls.PostTopic, subject.Id);

            var createdTopic = await PostAsync<TopicDTO, TopicModel>(formattedUrl, topicModel, TeacherTestUsername, TeacherTestUsernamePassword);

            createdTopic.Should().NotBeNull();
            createdTopic.Title.Should().Be(topicModel.Title);
            
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var topic = await unitOfWork.TopicRepository.GetByIdAsync(createdTopic.Id);
            topic.Should().NotBeNull();
        }

        private async Task PrepareTestData(IServiceScope scope, Subject subject)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var domainUserId = await PrepareTestUsers(scope, false);

            subject.TeacherId = domainUserId;
            unitOfWork.SubjectRepository.Add(subject);
            await unitOfWork.SaveAsync();
        }
    }
}
