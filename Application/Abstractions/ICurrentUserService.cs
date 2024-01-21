using Application.Identitity;

namespace Application.Abstractions
{
    public interface ICurrentUserService
    {
        string CurrentUserUserName { get; }

        bool IsInRole(ApplicationUserRole applicationRole);
    }
}
