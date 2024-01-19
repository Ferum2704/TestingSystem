using Application.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public new async Task<Topic?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            IQueryable<Topic> query = dbSet.Include(x => x.Tests).Include(x => x.Questions);

            return await dbSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
