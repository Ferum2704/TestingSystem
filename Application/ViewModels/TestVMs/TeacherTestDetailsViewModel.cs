using Application.ViewModels.StudentVMs;

namespace Application.ViewModels.TestVMs
{
    public class TeacherTestDetailsViewModel
    {
        public IReadOnlyCollection<StudentTestInfoViewModel> StudentResults { get; set; }
    }
}
