using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public new async Task<IReadOnlyCollection<Test>> GetAsync(Expression<Func<Test, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Test> query = dbSet.Include(x => x.TestQuestions);

            if (filter is null)
            {
                return await query.AsNoTracking().ToListAsync();
            }

            return await query.Where(filter).AsNoTracking().ToListAsync();
        }
    }
}
