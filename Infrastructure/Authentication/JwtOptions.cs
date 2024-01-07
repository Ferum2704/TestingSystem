using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; init; }

        public string Audience { get; init; }

        public string SecretKey { get; init; }

        public string AccessTokenExpirationTimeInMinutes { get; init; }

        public string RefreshTokenExpirationTimeInDays { get; init; }
    }
}
