﻿using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<IReadOnlyCollection<TEntity>> GetAll(List<string>? includeProperties = null);

        Task<IReadOnlyCollection<TEntity>> Find(Expression<Func<TEntity, bool>> filter, List<string>? includeProperties = null);

        void Add(TEntity entity);

        void AddRange(TEntity[] entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(TEntity[] entities);

        Task<TEntity?> GetById(Guid id, List<string>? includeProperties = null);
    }
}
