namespace Api.IntegrationTests
{
    public static class ApiUrls
    {
        public const string ApiBasePrefix = "api/testing-system";

        public const  string Login = $"/identity/login";

        public const string Register = $"/identity/register";

        public const string PostSubject = $"/subjects";

        public const string PostTopic = "/subjects/{0}/topics";

        public const string PostQuestion = "/subjects/{0}/topics{1}/questions";

        public const string PostTestQuestion = "/subjects/{0}/topics{1}/tests/{2}";

        public const string PostTest = "/subjects/{0}/topics/{1}/tests";
    }
}
