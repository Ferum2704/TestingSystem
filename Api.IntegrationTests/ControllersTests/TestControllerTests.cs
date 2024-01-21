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
    public class TestControllerTests : TestApiBase
    {
        public TestControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) : base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task PostTopic_ShouldCreateTestToTopic(
            Subject subjectEntity,
            Topic topicEntity,
            TestModel testModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            await PrepareTestData(scope, subjectEntity, topicEntity);
            var formattedUrl = string.Format(ApiUrls.PostTest, subjectEntity.Id, topicEntity.Id);

            var createdTest = await PostAsync<TestDTO, TestModel>(formattedUrl, testModel, TeacherTestUsername, TeacherTestUsernamePassword);

            createdTest.Should().NotBeNull();
            createdTest.Name.Should().Be(testModel.Name);
            createdTest.Duration.Should().Be(testModel.Duration);
            createdTest.NumberOfAttempts.Should().Be(testModel.NumberOfAttempts);
            createdTest.TestDate.Should().Be(testModel.TestDate);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var test = await unitOfWork.TestRepository.GetByIdAsync(createdTest.Id);
            test.Should().NotBeNull();
        }

        private async Task PrepareTestData(IServiceScope scope, Subject subject, Topic topic)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var domainUserId = await PrepareTestUsers(scope, false);

            subject.TeacherId = domainUserId;
            topic.SubjectId = subject.Id;
            unitOfWork.SubjectRepository.Add(subject);
            unitOfWork.TopicRepository.Add(topic);
            await unitOfWork.SaveAsync();
        }
    }
}
