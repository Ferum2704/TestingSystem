namespace Domain.Entities
{
    public class Student : DomainUser
    {
        public IReadOnlyCollection<Test> Tests { get; set; }

        public IReadOnlyCollection<StudentTestAttempt> TestsAttempts { get; set; }
    }
}
