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
        private ITestQuestionRepository testQuestionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IStudentTestResultRepository StudentTestResultRepository => studentTestResultRepository ??= new StudentTestResultRepository(context);

        public ITeacherRepository TeacherRepository => teacherRepository ??= new TeacherRepository(context);

        public ISubjectRepository SubjectRepository => subjectRepository ??= new SubjectRepository(context);

        public ITopicRepository TopicRepository => topicRepository ??= new TopicRepository(context);

        public ITestRepository TestRepository => testRepository ??= new TestRepository(context);

        public IQuestionRepository QuestionRepository => questionRepository ??= new QuestionRepository(context);

        public IStudentRepository StudentRepository => studentRepository ??= new StudentRepository(context);

        public IStudentTestAttemptRepository StudentTestAttemptRepository => studentTestAttemptRepository ??= new StudentTestAttemptRepository(context);

        public ITestQuestionRepository TestQuestionRepository => testQuestionRepository ??= new TestQuestionRepository(context);

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
