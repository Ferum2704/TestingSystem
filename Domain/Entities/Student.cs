namespace Domain.Entities
{
    public class Student : DomainUser
    {
        public int NumberOfAttemts { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<StudentTestAttempt> TestsAttempts { get; set; }
    }
}
