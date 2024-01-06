using Domain.Entities.DomainEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual DomainUser DomainUser { get; set; }
    }
}
