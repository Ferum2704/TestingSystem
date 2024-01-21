using Application.DTOs;
using Presentation.Api.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Api.IntegrationTests
{
    public abstract class TestApiBase
    {
        private readonly HttpClient httpClient;

        public TestApiBase(WebApplicationFactoryFixture webApplicationFactoryFixture)
        {
            WebApplicationFactory = webApplicationFactoryFixture;
            httpClient = WebApplicationFactory.HttpClient;
        }

        protected WebApplicationFactoryFixture WebApplicationFactory { get; private set; }

        protected string StudentTestUsername => "Student1";

        protected string StudentTestUsernamePassword => "Student123!";

        protected string TeacherTestUsername => "Teacher1";

        protected string TeacherTestUsernamePassword => "Teacher123!";


        public async Task<TResponse> PostAsync<TResponse, TRequest>(
            string relativeURL, 
            TRequest request,
            string userLogin = null,
            string userPassword = null,
            bool isAuthorizationRequired = true)
        {
            AuthorizationToken token;
            if (isAuthorizationRequired)
            {
                token = await GetAuthorizationToken(userLogin, userPassword);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }

            var response = await httpClient.PostAsJsonAsync($"{ApiUrls.ApiBasePrefix}{relativeURL}", request);

            //response.EnsureSuccessStatusCode();

            var sringresponse = await response.Content.ReadAsStringAsync();

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        private async Task<AuthorizationToken> GetAuthorizationToken(string userLogin, string userPassword)
        {
            var response = await httpClient.PostAsJsonAsync(ApiUrls.Login, new LoginDTO { Username = userLogin, Password = userPassword });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthorizationToken>();
        }
    }
}
