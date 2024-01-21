using Api.IntegrationTests.AutoFixture;
using Application.DTOs;
using Application.Identitity;
using Application.ViewModels;
using FluentAssertions;
using System.Net.Http.Json;
using Xunit;

namespace Api.IntegrationTests.ControllersTests
{
    [Collection("SharedWebFactory")]
    public class IdentityControllerTests : TestApiBase, IAsyncLifetime
    {
        public IdentityControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) : 
            base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task Register_ShouldRegisterAsTeacher(RegistrationDTO registrationModel)
        {
            registrationModel.Role = ApplicationUserRole.Teacher;

            var response = await WebApplicationFactory.HttpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Register}", registrationModel);

            response.EnsureSuccessStatusCode();
        }

        [Theory, AutoMoqData]
        public async Task Register_ShouldRegisterAsStudent(RegistrationDTO registrationModel)
        {
            registrationModel.Role = ApplicationUserRole.Student;

            var response = await WebApplicationFactory.HttpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Register}", registrationModel);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_ShouldLoginAsStudent()
        {
            await PrepareTestUsers(true);

            var LoginDTO = new LoginDTO
            {
                Username = StudentTestUsername,
                Password = StudentTestUsernamePassword,
            };

            var tokenViewModel = await PostAsync<TokenViewModel, LoginDTO>($"{ApiUrls.Login}", LoginDTO, isAuthorizationRequired: false);

            tokenViewModel.Should().NotBeNull();
            tokenViewModel.Tokens.AccessToken.Should().NotBeNull();
            tokenViewModel.Tokens.RefreshToken.Should().NotBeNull();
            tokenViewModel.DomainUserId.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldLoginAsTeacher()
        {
            await PrepareTestUsers(false);

            var LoginDTO = new LoginDTO
            {
                Username = TeacherTestUsername,
                Password = TeacherTestUsernamePassword,
            };

            var tokenViewModel = await PostAsync<TokenViewModel, LoginDTO>($"{ApiUrls.Login}", LoginDTO, isAuthorizationRequired: false);

            tokenViewModel.Should().NotBeNull();
            tokenViewModel.Tokens.AccessToken.Should().NotBeNull();
            tokenViewModel.Tokens.RefreshToken.Should().NotBeNull();
            tokenViewModel.DomainUserId.Should().NotBeNull();
        }
    }
}
