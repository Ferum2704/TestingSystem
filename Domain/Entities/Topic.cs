namespace Domain.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid SubjectId { get; set; }

        public Subject Subject { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}
