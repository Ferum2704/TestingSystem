using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class TestQuestionRepository : GenericRepository<TestQuestion>, ITestQuestionRepository
    {
        public TestQuestionRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public new async Task<IReadOnlyCollection<TestQuestion>> GetAsync(Expression<Func<TestQuestion, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TestQuestion> query = dbSet.Include(x => x.Question);

            if (filter is null)
            {
                return await query.AsNoTracking().ToListAsync(cancellationToken);
            }

            return await query.Where(filter).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
