using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public new async Task<IReadOnlyCollection<StudentTestAttempt>> GetAsync(Expression<Func<StudentTestAttempt, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<StudentTestAttempt> query = dbSet.Include(x => x.Results);

            if (filter is null)
            {
                return await query.AsNoTracking().ToListAsync(cancellationToken);
            }

            return await query.Where(filter).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
