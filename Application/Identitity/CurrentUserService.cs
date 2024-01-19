using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Identitity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

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
