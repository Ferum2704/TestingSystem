namespace Domain.Entities
{
    public class Teacher : DomainUser
    {
        public ICollection<Subject> Subjects { get; set; }
    }
}
