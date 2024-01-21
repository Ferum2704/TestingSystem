using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public new async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await dbSet.Include(x => x.TestsAttempts).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
