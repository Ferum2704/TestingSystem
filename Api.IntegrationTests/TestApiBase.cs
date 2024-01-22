using Application.Abstractions;
using Application.DTOs;
using Application.Identitity;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Repositories;
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
            var response = await PostAsync<TRequest>(relativeURL, request, userLogin, userPassword, isAuthorizationRequired);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PostAsync<TRequest>(
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

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<TRequest>(
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

            var response = await httpClient.PutAsJsonAsync($"{ApiUrls.ApiBasePrefix}{relativeURL}", request);

            response.EnsureSuccessStatusCode();

            return response;
        }

        protected async Task<(Guid TeacherId, Guid StudentId)> PrepareTestUsers(IServiceScope scope, bool isStudent, bool createBoth = false)
        {
            Guid teacherId = Guid.Empty;
            Guid studentId = Guid.Empty;

            if (createBoth)
            {
                studentId = await CreateStudent(scope);
                teacherId = await CreateTeacher(scope);

                return (teacherId, studentId);
            }
            else if (isStudent)
            {
                studentId = await CreateStudent(scope);
            }
            else
            {
                teacherId = await CreateTeacher(scope);
            }

            return (teacherId, studentId);
        }

        private async Task<Guid> CreateStudent(IServiceScope scope)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var appUser = await CreateAppUser(scope, StudentTestUsername, StudentTestUsernamePassword, ApplicationUserRole.Student.ToString());
            var student = new Student();
            var studentId = Guid.NewGuid();
            student.Id = studentId;
            student.Name = "Bob";
            student.Surname = "Gomes";
            student.ApplicationUserId = appUser.Id;

            unitOfWork.StudentRepository.Add(student);
            await unitOfWork.SaveAsync();

            return student.Id;
        }

        private async Task<Guid> CreateTeacher(IServiceScope scope)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var appUser = await CreateAppUser(scope, TeacherTestUsername, TeacherTestUsernamePassword, ApplicationUserRole.Teacher.ToString());
            var teacher = new Teacher();
            var teacherId = Guid.NewGuid();
            teacher.Id = teacherId;
            teacher.Name = "Bob";
            teacher.Surname = "Gomes";
            teacher.ApplicationUserId = appUser.Id;

            unitOfWork.TeacherRepository.Add(teacher);
            await unitOfWork.SaveAsync();

            return teacher.Id;
        }

        private async Task<ApplicationUser> CreateAppUser(IServiceScope scope, string userName, string userPassword, string role)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var appUser = new ApplicationUser
            {
                UserName = userName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = role,
            });
            await userManager.CreateAsync(appUser, userPassword);
            await userManager.AddToRoleAsync(appUser, role);

            return appUser;
        }

        private async Task<TokenViewModel> GetAuthorizationToken(string userLogin, string userPassword)
        {
            var response = await httpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{ApiUrls.Login}", new LoginDTO { Username = userLogin, Password = userPassword });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TokenViewModel>();
        }
    }
}
