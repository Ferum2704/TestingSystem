using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddRange(TEntity[] entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual IReadOnlyCollection<TEntity> Find(Expression<Func<TEntity, bool>> filter, List<string>? includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return query.Where(filter).ToList();
        }

        public virtual IReadOnlyCollection<TEntity> GetAll(List<string>? includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return query.ToList();
        }

        public virtual TEntity? GetById(Guid id, List<string>? includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return dbSet.Find(id);
        }

        public virtual void Remove(TEntity entity) => dbSet.Remove(entity);

        public virtual void RemoveRange(TEntity[] entities) => dbSet.RemoveRange(entities);

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
