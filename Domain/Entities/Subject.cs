using Domain.Interfaces;

namespace Domain.Entities
{
    public class Subject : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public IReadOnlyCollection<Topic> Topics { get; set; }
    }
}
