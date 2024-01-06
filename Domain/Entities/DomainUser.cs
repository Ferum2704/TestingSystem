namespace Domain.Entities
{
    public abstract class DomainUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
}
