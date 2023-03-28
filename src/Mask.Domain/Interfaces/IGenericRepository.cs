using Mask.Domain.Entities;
using System.Linq.Expressions;

namespace Mask.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TPrimarykey, ForeignKey> : IDisposable 
        where TEntity : IEntity<TPrimarykey, ForeignKey> 
        where TPrimarykey : IComparable
        where ForeignKey : IComparable
    {
        public IQueryable<TEntity> GetAllQuery();

        public Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default);

        public IEnumerable<TEntity> GetAll();

        public Task<TEntity?> GetByIdAsync(TPrimarykey id, CancellationToken cancellationToken = default);

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task DeleteAsync(TEntity entity);

        public Task DeleteAsync(TPrimarykey id, CancellationToken cancellationToken = default);

        public Task CreateManyAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        public Task<TEntity[]> UpdateManyAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        public Task<TEntity[]> DeleteManyAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        public Task<TEntity[]> DeleteManyAsync(TPrimarykey[] entitieIds, CancellationToken cancellationToken = default);

        public Task<bool> SaveChangesAsync();
    }
}
