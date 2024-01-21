using Application.Abstractions;
using Application.Identitity;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserUserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

        public bool IsInRole(ApplicationUserRole applicationRole)
        {
            var user = httpContextAccessor.HttpContext.User;

            if (user == null)
            {
                return false;
            }

            return user.IsInRole(applicationRole.ToString());
        }
    }
}
