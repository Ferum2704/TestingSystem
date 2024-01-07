using Domain.Interfaces;

namespace Domain.Entities
{
    public class StudentTestAttempt : IEntity
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid TestId { get; set; }

        public Test Test { get; set; }

        public int NumberOfAttemts { get; set; }
    }
}
