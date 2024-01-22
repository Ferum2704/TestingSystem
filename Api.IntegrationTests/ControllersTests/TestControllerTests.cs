using Api.IntegrationTests.AutoFixture;
using Application.Abstractions;
using Application.DTOs;
using Application.DTOs.Enums;
using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Models;
using System.Threading;
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
        public async Task PostTest_ShouldCreateTestToTopic(
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

        [Theory, AutoMoqData]
        public async Task PutTest_ShouldChangeTestStateToInProgress(
            Subject subjectEntity,
            Topic topicEntity,
            Test testEntity,
            StudentTestAttempt[] studentTestAttempts,
            StudentTestAttemptPutModel testModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            testModel.State = TestStateDTO.InProgress;
            var studentId = await PrepareTestData(scope, subjectEntity, topicEntity, testEntity, studentTestAttempts);
            var formattedUrl = string.Format(ApiUrls.PutTest, subjectEntity.Id, topicEntity.Id, testEntity.Id, studentId);

            var createdTest = await PutAsync(formattedUrl, testModel, StudentTestUsername, StudentTestUsernamePassword);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var testAttempt = (await unitOfWork.StudentTestAttemptRepository
                .GetAsync(x => x.TestId == testEntity.Id && x.StudentId == studentId && x.State == TestState.InProgress))
                .OrderBy(x => x.NumberOfAttemt)
                .FirstOrDefault();
            testAttempt.Should().NotBeNull();
            testAttempt.StartedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1.0));
        }
        private async Task<Guid> PrepareTestData(
        IServiceScope scope,
        Subject subject, 
            Topic topic, 
            Test test = null, 
            StudentTestAttempt[] studentTestAttempts = null)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var (teacherId, studentId) = await PrepareTestUsers(scope, true, true);

            subject.TeacherId = teacherId;
            topic.SubjectId = subject.Id;
            unitOfWork.SubjectRepository.Add(subject);
            unitOfWork.TopicRepository.Add(topic);
            await unitOfWork.SaveAsync();

            if (test is not null)
            {
                test.TopicId = topic.Id;
                unitOfWork.TestRepository.Add(test);
                await unitOfWork.SaveAsync();
            }

            if (studentTestAttempts is not null && test is not null)
            {
                for (int i = 0, j = 0; i < studentTestAttempts.Length; i++)
                {
                    studentTestAttempts[i].NumberOfAttemt = ++j;
                    studentTestAttempts[i].TestId = test.Id;
                    studentTestAttempts[i].StudentId = studentId;
                    studentTestAttempts[i].State = TestState.NotStarted;
                    unitOfWork.StudentTestAttemptRepository.Add(studentTestAttempts[i]);
                    await unitOfWork.SaveAsync();
                }
            }

            return studentId;
        }
    }
}
