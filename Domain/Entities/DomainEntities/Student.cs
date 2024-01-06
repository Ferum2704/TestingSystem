namespace Domain.Entities.DomainEntities
{
    public class Student : DomainUser
    {
        public ICollection<Test> Tests { get; set; }

        public ICollection<StudentTestAttempt> TestsAttempts { get; set; }

        public ICollection<StudentTestResult> TestResults { get; set; }
    }
}
