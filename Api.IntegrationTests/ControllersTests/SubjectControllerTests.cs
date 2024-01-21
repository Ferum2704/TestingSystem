using Api.IntegrationTests.AutoFixture;
using Application.Abstractions;
using Application.DTOs;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Models;
using Xunit;

namespace Api.IntegrationTests.ControllersTests
{
    [Collection("SharedWebFactory")]
    public class SubjectControllerTests : TestApiBase
    {
        public SubjectControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) :
            base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task PostSubject_ShouldCreateSubjectForTeacher(SubjectModel subjectModel)
        {
            await PrepareTestUsers(false);

            var createdSubject = await PostAsync<SubjectDTO, SubjectModel>(ApiUrls.PostSubject, subjectModel, TeacherTestUsername, TeacherTestUsernamePassword);
            
            createdSubject.Should().NotBeNull();
            createdSubject.Name.Should().Be(subjectModel.Name);

            using var scope = WebApplicationFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(createdSubject.Id);
            subject.Should().NotBeNull();
        }
    }
}
