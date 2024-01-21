using Application.Abstractions;
using Application.DTOs;
using Application.Identitity;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Api.IntegrationTests
{
    public abstract class TestApiBase : IAsyncLifetime
    {
        private readonly Func<Task> resetDatabase;

        private readonly HttpClient httpClient;

        public TestApiBase(WebApplicationFactoryFixture webApplicationFactoryFixture)
        {
            WebApplicationFactory = webApplicationFactoryFixture;
            resetDatabase = webApplicationFactoryFixture.ResetDatabase;
            httpClient = webApplicationFactoryFixture.HttpClient;
        }

        protected WebApplicationFactoryFixture WebApplicationFactory { get; private set; }

        protected string StudentTestUsername => "Student1";

        protected string StudentTestUsernamePassword => "Student123!";

        protected string TeacherTestUsername => "Teacher1";

        protected string TeacherTestUsernamePassword => "Teacher123!";

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await resetDatabase();

        public async Task<TResponse> PostAsync<TResponse, TRequest>(
            string relativeURL, 
            TRequest request,
            string userLogin = null,
            string userPassword = null,
            bool isAuthorizationRequired = true)
        {
            TokenViewModel token;
            if (isAuthorizationRequired)
            {
                token = await GetAuthorizationToken(userLogin, userPassword);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Tokens.AccessToken);
            }

            var response = await httpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{relativeURL}", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        protected async Task<Guid> PrepareTestUsers(bool isStudent)
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
            var domainUserId = Guid.NewGuid();
            domainUser.Id = domainUserId;
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

            return domainUserId;
        }

        private async Task<TokenViewModel> GetAuthorizationToken(string userLogin, string userPassword)
        {
            var response = await httpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Login}", new LoginDTO { Username = userLogin, Password = userPassword });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TokenViewModel>();
        }
    }
}
