namespace Domain.Entities
{
    public class Teacher : DomainUser
    {
        public IReadOnlyCollection<Subject> Subjects { get; set; }
    }
}
