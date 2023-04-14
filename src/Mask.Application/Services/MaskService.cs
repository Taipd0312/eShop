using Mask.Application.Interfaces;
using Mask.Application.UnitOfWorks;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;

namespace Mask.Application.Services
{
    public class MaskService<TEntity, TPrimaryKey, TForeignKey> : MaskCoreService<TEntity, TPrimaryKey, TForeignKey>, IMaskService<TEntity, TPrimaryKey, TForeignKey>, IMaskCoreService<TEntity, TPrimaryKey, TForeignKey>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public MaskService(IUnitOfWork uOW, IGenericRepository<TEntity, TPrimaryKey, TForeignKey> genericRepository) : base(uOW, genericRepository)
        {
        }
    }

    public abstract class MaskCoreService : IMaskCoreService
    {
        public readonly IUnitOfWork UOW;

        protected MaskCoreService(IUnitOfWork uOW)
        {
            UOW = uOW;
        }
    }

    public abstract class MaskCoreService<TEntity, TPrimaryKey, TForeignKey> : IMaskCoreService<TEntity, TPrimaryKey, TForeignKey>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        private readonly IUnitOfWork UOW;
        private readonly IGenericRepository<TEntity, TPrimaryKey, TForeignKey> genericRepository;

        protected MaskCoreService(IUnitOfWork uOW, IGenericRepository<TEntity, TPrimaryKey, TForeignKey> genericRepository)
        {
            UOW = uOW;
            this.genericRepository = genericRepository;
        }

        public virtual async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await genericRepository.CreateAsync(entity, cancellationToken);
            if (UOW.Commit() > 0) return entity;

            throw new ApplicationException("Insert failured!");
        }

        public virtual async Task<TEntity> DeleteEntityAsync(TEntity entity)
        {
            await genericRepository.DeleteAsync(entity);
            if (UOW.Commit() > 0) return entity;

            throw new ApplicationException("Detele failured!");
        }

        public virtual async Task DeleteEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await genericRepository.FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken: cancellationToken);

            if (entity != null)
            {
                await DeleteEntityAsync(entity);
            }

            throw new ApplicationException($"Not found {typeof(TEntity)} id {id}");
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await genericRepository.GetAllAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await genericRepository.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (entity != null)
            {
                return entity;
            }

            throw new ApplicationException($"Not found {typeof(TEntity)} id {id}");
        }

        public virtual async Task<TEntity> UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await genericRepository.UpdateAsync(entity);
            if (UOW.Commit() > 0) return entity;

            throw new ApplicationException("Update Failured!");
        }
    }
}
