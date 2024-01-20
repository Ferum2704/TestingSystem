using Api.IntegrationTests.AutoFixture;
using Application.Abstractions;
using Application.Identitity;
using Application.ViewModels;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Models;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using Xunit;

namespace Api.IntegrationTests.ControllersTests
{
    public class IdentityControllerTests : TestApiBase, IClassFixture<WebApplicationFactoryFixture>
    {
        public IdentityControllerTests(WebApplicationFactoryFixture webApplicationFactoryFixture) : 
            base(webApplicationFactoryFixture)
        {
        }

        [Theory, AutoMoqData]
        public async Task Register_ShouldRegisterAsTeacher(RegistrationModel registrationModel)
        {
            registrationModel.Role = ApplicationUserRole.Teacher;

            var response = await WebApplicationFactory.HttpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Register}", registrationModel);

            response.EnsureSuccessStatusCode();
        }

        [Theory, AutoMoqData]
        public async Task Register_ShouldRegisterAsStudent(RegistrationModel registrationModel)
        {
            registrationModel.Role = ApplicationUserRole.Student;

            var response = await WebApplicationFactory.HttpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Register}", registrationModel);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_ShouldLoginAsStudent()
        {
            await PrepareTestData(true);

            var loginModel = new LoginModel
            {
                Username = StudentTestUsername,
                Password = StudentTestUsernamePassword,
            };

            var tokenViewModel = await PostAsync<TokenViewModel, LoginModel>($"{ApiUrls.Login}", loginModel, isAuthorizationRequired: false);

            tokenViewModel.Should().NotBeNull();
            tokenViewModel.AccessToken.Should().NotBeNull();
            tokenViewModel.RefreshToken.Should().NotBeNull();
            tokenViewModel.DomainUserId.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldLoginAsTeacher()
        {
            await PrepareTestData(false);

            var loginModel = new LoginModel
            {
                Username = TeacherTestUsername,
                Password = TeacherTestUsernamePassword,
            };

            var tokenViewModel = await PostAsync<TokenViewModel, LoginModel>($"{ApiUrls.Login}", loginModel, isAuthorizationRequired: false);

            tokenViewModel.Should().NotBeNull();
            tokenViewModel.AccessToken.Should().NotBeNull();
            tokenViewModel.RefreshToken.Should().NotBeNull();
            tokenViewModel.DomainUserId.Should().NotBeNull();
        }

        private async Task PrepareTestData(bool isStudent)
        {
            using var scope = WebApplicationFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();



            var appUser = new ApplicationUser
            {
                UserName = isStudent ? StudentTestUsername : TeacherTestUsername,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = isStudent ? ApplicationUserRole.Student.ToString() : ApplicationUserRole.Teacher.ToString(),
            });
            await userManager.CreateAsync(appUser, isStudent ? StudentTestUsernamePassword : TeacherTestUsernamePassword);
            await userManager.AddToRoleAsync(appUser, isStudent ? ApplicationUserRole.Student.ToString() : ApplicationUserRole.Teacher.ToString());

            DomainUser domainUser = isStudent ? new Student() : new Teacher();
            domainUser.Id = Guid.NewGuid();
            domainUser.Name = "Joe";
            domainUser.Surname = "Gomes";
            domainUser.ApplicationUserId = appUser.Id;

            if (isStudent)
            {
                unitOfWork.StudentRepository.Add((Student)domainUser);
            }
            else
            {
                unitOfWork.TeacherRepository.Add((Teacher)domainUser);
            }

            await unitOfWork.SaveAsync();
        }
    }
}
