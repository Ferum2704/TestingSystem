using Application.Identitity;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface ICurrentUserService
    {
        string CurrentUserUserName { get; }

        bool IsInRole(ApplicationUserRole applicationRole);

        Task<DomainUser> GetCurrentDomainUserAsync();
    }
}
