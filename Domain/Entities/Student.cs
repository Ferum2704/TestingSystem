namespace Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<StudentTestAttempt> TestsAttempts { get; set; }

        public ICollection<StudentTestResult> TestResults { get; set; }
    }
}
