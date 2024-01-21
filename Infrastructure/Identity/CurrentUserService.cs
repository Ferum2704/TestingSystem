using Application.Abstractions;
using Application.Identitity;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public string CurrentUserUserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

        public async Task<DomainUser> GetCurrentDomainUserAsync()
        {
            var user = await userManager.FindByNameAsync(CurrentUserUserName);

            return user.DomainUser;
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
