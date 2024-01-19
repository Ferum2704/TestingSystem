using Application.Identitity;

namespace Application.Abstractions
{
    public interface ICurrentUserService
    {
        bool IsInRole(ApplicationUserRole applicationRole);
    }
}
