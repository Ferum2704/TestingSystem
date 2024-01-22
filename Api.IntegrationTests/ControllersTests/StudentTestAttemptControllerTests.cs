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
    public class StudentTestAttemptControllerTests : TestApiBase
    {
        public StudentTestAttemptControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) : base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task PostStudentTestAttempt_ShouldCreateStudentAttemptsToTest(
            Subject subjectEntity,
            Topic topicEntity,
            Test testEntity)
        {
            using var scope = WebApplicationFactory.CreateScope();
            var studentId = await PrepareTestData(scope, subjectEntity, topicEntity, testEntity);
            var formattedUrl = string.Format(ApiUrls.PostStudentAttempt, subjectEntity.Id, topicEntity.Id, testEntity.Id);

            var createdStudentAttempts = await PostAsync<IReadOnlyCollection<Guid>, StudentTestAttemptPostModel>(
                formattedUrl, 
                new StudentTestAttemptPostModel { StudentId = studentId }, 
                TeacherTestUsername, 
                TeacherTestUsernamePassword);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            createdStudentAttempts.Count.Should().Be(testEntity.NumberOfAttempts);
            foreach (var attempt in createdStudentAttempts)
            {
                var studentTestAttempt = await unitOfWork.StudentTestAttemptRepository.GetByIdAsync(attempt);
                studentTestAttempt.Should().NotBeNull();
            }
        }

        private async Task<Guid> PrepareTestData(IServiceScope scope, Subject subject, Topic topic, Test test)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var (teacherId, studentId) = await PrepareTestUsers(scope, false, true);

            subject.TeacherId = teacherId;
            topic.SubjectId = subject.Id;
            unitOfWork.SubjectRepository.Add(subject);
            unitOfWork.TopicRepository.Add(topic);
            await unitOfWork.SaveAsync();

            test.TopicId = topic.Id;
            unitOfWork.TestRepository.Add(test);
            await unitOfWork.SaveAsync();

            return studentId;
        }
    }
}
