namespace Application.ViewModels.TestVMs
{
    public class TopicTestViewModel
    {
        public Guid Id { get; set; }

        public DateTime TestDate { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public int NumberOfAttempts { get; set; }

        public IReadOnlyCollection<Guid> QuestionIds { get; set; }
    }
}
