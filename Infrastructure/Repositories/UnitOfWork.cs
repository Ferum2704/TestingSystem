using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        private bool disposed = false;
        private IGenericRepository<Subject> subjectRepository;
        private IGenericRepository<Topic> topicRepository;
        private IGenericRepository<Test> testRepository;
        private IGenericRepository<Question> questionRepository;
        private IGenericRepository<Student> studentRepository;
        private IGenericRepository<StudentTestAttempt> studentTestAttemptRepository;

        public IGenericRepository<Subject> SubjectRepository => subjectRepository ??= new GenericRepository<Subject>(context);

        public IGenericRepository<Topic> TopicRepository => topicRepository ??= new GenericRepository<Topic>(context);

        public IGenericRepository<Test> TestRepository => testRepository ??= new GenericRepository<Test>(context);

        public IGenericRepository<Question> QuestionRepository => questionRepository ??= new GenericRepository<Question>(context);

        public IGenericRepository<Student> StudentRepository => studentRepository ??= new GenericRepository<Student>(context);

        public IGenericRepository<StudentTestAttempt> StudentTestAttemptRepository => studentTestAttemptRepository ??= new GenericRepository<StudentTestAttempt>(context);

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
