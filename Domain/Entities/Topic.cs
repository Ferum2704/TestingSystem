using Domain.Interfaces;

namespace Domain.Entities
{
    public class Topic : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid SubjectId { get; set; }

        public Subject Subject { get; set; }

        public IReadOnlyCollection<Test> Tests { get; set; }

        public IReadOnlyCollection<Question> Questions { get; set; }
    }
}
