using Mask.Domain.Entities;

namespace Mask.Application.Interfaces
{
    public interface IMaskService<TEntity, TPrimaryKey, TForeignKey> : IMaskCoreService<TEntity, TPrimaryKey, TForeignKey>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public interface IMaskCoreService
    {
    }

    public interface IMaskCoreService<TEntity, TPrimaryKey, TForeignKey> 
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken = default);

        public Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        public Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task<TEntity> UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task<TEntity> DeleteEntityAsync(TEntity entity);

        public Task DeleteEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
    }
}
