using Domain.Interfaces;

namespace Domain.Entities
{
    public abstract class DomainUser : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
}
