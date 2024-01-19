using Application.Abstractions.IRepository;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class StudentTestResultRepository : GenericRepository<StudentTestResult>, IStudentTestResultRepository
    {
        public StudentTestResultRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
