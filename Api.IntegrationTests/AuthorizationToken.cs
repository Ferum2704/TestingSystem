namespace Api.IntegrationTests
{
    public class AuthorizationToken
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public Guid? DomainUserId { get; set; }
    }
}
