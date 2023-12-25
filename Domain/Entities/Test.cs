namespace Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }

        public DateTime TestDate { get; set; }

        public Guid StudentId { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<StudentTestAttempt> Students { get; set; }
    }
}
