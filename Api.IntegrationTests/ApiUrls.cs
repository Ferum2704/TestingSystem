namespace Api.IntegrationTests
{
    public static class ApiUrls
    {
        public const string ApiBasePrefix = "api/testing-system";

        public const  string Login = $"/identity/login";

        public const string Register = $"/identity/register";

        public const string PostSubject = $"/subjects";

        public const string PostTopic = "/subjects/{0}/topics";
    }
}
