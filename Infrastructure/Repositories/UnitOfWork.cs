using Application.Abstractions;
using Application.Abstractions.IRepository;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        private ITeacherRepository teacherRepository;
        private ISubjectRepository subjectRepository;
        private ITopicRepository topicRepository;
        private ITestRepository testRepository;
        private IQuestionRepository questionRepository;
        private IStudentRepository studentRepository;
        private IStudentTestAttemptRepository studentTestAttemptRepository;
        private IStudentTestResultRepository studentTestResultRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IStudentTestResultRepository StudentTestResultRepository => studentTestResultRepository ??= new StudentTestResultRepository(context);

        ITeacherRepository IUnitOfWork.TeacherRepository => teacherRepository ??= new TeacherRepository(context);

        ISubjectRepository IUnitOfWork.SubjectRepository => subjectRepository ??= new SubjectRepository(context);

        ITopicRepository IUnitOfWork.TopicRepository => topicRepository ??= new TopicRepository(context);

        ITestRepository IUnitOfWork.TestRepository => testRepository ??= new TestRepository(context);

        IQuestionRepository IUnitOfWork.QuestionRepository => questionRepository ??= new QuestionRepository(context);

        IStudentRepository IUnitOfWork.StudentRepository => studentRepository ??= new StudentRepository(context);

        IStudentTestAttemptRepository IUnitOfWork.StudentTestAttemptRepository => studentTestAttemptRepository ??= new StudentTestAttemptRepository(context);

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
