using Application.Abstractions.IRepository;

namespace Application.Abstractions
{
    public interface IUnitOfWork
    {
        ITeacherRepository TeacherRepository { get; }

        ISubjectRepository SubjectRepository { get; }

        ITopicRepository TopicRepository { get; }

        ITestRepository TestRepository { get; }

        IQuestionRepository QuestionRepository { get; }

        IStudentRepository StudentRepository { get; }

        IStudentTestAttemptRepository StudentTestAttemptRepository { get; }

        IStudentTestResultRepository StudentTestResultRepository { get; }

        ITestQuestionRepository TestQuestionRepository { get; }

        Task SaveAsync();
    }
}
