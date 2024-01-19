using Application.DTOs.Enums;

namespace Application.ViewModels
{
    public class StudentTestAttemptViewModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public int NumberOfAttemt { get; set; }

        public TestStateDTO State { get; set; }

        public IReadOnlyCollection<StudentTestResultViewModel> Results { get; set; }
    }
}
