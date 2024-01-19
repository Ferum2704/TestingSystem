using Application.Abstractions.IRepository;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class StudentTestAttemptRepository : GenericRepository<StudentTestAttempt>, IStudentTestAttemptRepository
    {
        public StudentTestAttemptRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
