namespace Application.ViewModels.StudentVMs
{
    public class StudentTestInfoViewModel
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<StudentTestAttemptViewModel> Attempts { get; set; }
    }
}
