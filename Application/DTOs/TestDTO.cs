namespace Application.DTOs
{
    public class TestDTO
    {
        public Guid Id { get; set; }

        public DateTime TestDate { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public int NumberOfAttempts { get; set; }
    }
}
