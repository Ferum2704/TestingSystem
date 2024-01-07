using Domain.Entities;
using Domain.Enums;
using System.Security.Claims;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        public string GetAccessToken(ApplicationUser user, string userRole);

        public string GetRefreshToken();

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    }
}
