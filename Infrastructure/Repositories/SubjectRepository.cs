﻿using Application.Abstractions.IRepository;
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

        public new async Task<IReadOnlyCollection<Subject>> GetAsync(Expression<Func<Subject, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Subject> query = dbSet.Include(x => x.Topics);

            if (filter is null)
            {
                await query.AsNoTracking().ToListAsync();
            }

            return await query.Where(filter).AsNoTracking().ToListAsync();
        }

        public new async Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            IQueryable<Subject> query = dbSet.Include(x => x.Topics);

            return await dbSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
