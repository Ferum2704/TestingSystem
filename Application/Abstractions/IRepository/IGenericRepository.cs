using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Abstractions.IRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
