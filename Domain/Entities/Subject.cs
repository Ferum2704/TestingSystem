namespace Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
