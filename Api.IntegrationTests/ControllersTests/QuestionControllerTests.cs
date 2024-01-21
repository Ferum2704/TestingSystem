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
            Subject subjectEntity,
            Topic topicEntity,
            QuestionModel questionModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            await PrepareTestData(scope, subjectEntity, topicEntity);
            var formattedUrl = string.Format(ApiUrls.PostQuestion, subjectEntity.Id, topicEntity.Id);

            var createdQuestion = await PostAsync<QuestionDTO, QuestionModel>(formattedUrl, questionModel, TeacherTestUsername, TeacherTestUsernamePassword);

            createdQuestion.Should().NotBeNull();
            createdQuestion.CorrectAnswer.Should().Be(questionModel.CorrectAnswer);
            createdQuestion.OptionA.Should().Be(questionModel.OptionA);
            createdQuestion.OptionB.Should().Be(questionModel.OptionB);
            createdQuestion.OptionC.Should().Be(questionModel.OptionC);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var question = await unitOfWork.QuestionRepository.GetByIdAsync(createdQuestion.Id);
            question.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public async Task PostQuestion_ShouldAddQuestionToTest(
            Subject subjectEntity,
            Topic topicEntity,
            Test testEntity,
            Question questionEntity,
            TestQuestionModel testQuestionModel)
        {
            using var scope = WebApplicationFactory.CreateScope();
            await PrepareTestData(scope, subjectEntity, topicEntity, testEntity, questionEntity);
            testQuestionModel.QuestionId = questionEntity.Id;
            var formattedUrl = string.Format(ApiUrls.PostTestQuestion, subjectEntity.Id, topicEntity.Id, testEntity.Id);

            await PostAsync(formattedUrl, testQuestionModel, TeacherTestUsername, TeacherTestUsernamePassword);

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var testQuestion = (await unitOfWork.TestQuestionRepository.GetAsync(x => x.TestId == testEntity.Id && x.QuestionId == questionEntity.Id)).FirstOrDefault();
            testQuestion.Should().NotBeNull();
        }

        private async Task PrepareTestData(IServiceScope scope, Subject subject, Topic topic, Test test = null, Question question = null)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var domainUserId = await PrepareTestUsers(scope, false);

            subject.TeacherId = domainUserId;
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

            if (question is not null)
            {
                question.TopicId = topic.Id;
                unitOfWork.QuestionRepository.Add(question);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
