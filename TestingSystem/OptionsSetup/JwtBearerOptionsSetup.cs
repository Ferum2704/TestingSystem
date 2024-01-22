using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.OptionsSetup
{
    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtOptions jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
        {
            jwtOptions = options.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(JwtBearerDefaults.AuthenticationScheme, options);
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            };
        }
    }
}