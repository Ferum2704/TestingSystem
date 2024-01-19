using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentTestAttemptRepository : GenericRepository<StudentTestAttempt>, IStudentTestAttemptRepository
    {
        public StudentTestAttemptRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public new async Task<StudentTestAttempt?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await dbSet.Include(x => x.Results).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
