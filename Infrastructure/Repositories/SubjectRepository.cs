using Application.Abstractions.IRepository;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public new async Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await dbSet.Include(x => x.Topics).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
