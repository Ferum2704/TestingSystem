using Application.ViewModels.QuestionVMs;

namespace Application.ViewModels.TestVMs
{
    public class StudentTestDetailsViewModel
    {
        public IReadOnlyCollection<TestQuestionViewModel> Questions { get; set; }

        public IReadOnlyCollection<StudentTestAttemptViewModel> Attempts { get; set; }
    }
}
