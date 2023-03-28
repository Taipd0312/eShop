using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TPrimarykey, TForeignKey> : IGenericRepository<TEntity, TPrimarykey, TForeignKey>
        where TEntity : AudiedEntity<TPrimarykey, TForeignKey>
        where TPrimarykey : IComparable
        where TForeignKey : IComparable
    {
        internal MaskDbContext context;
        internal DbSet<TEntity> Table;

        public GenericRepository(MaskDbContext context)
        {
            this.context = context;
            this.Table = context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Table.AddAsync(entity, cancellationToken);
        }

        public virtual async Task CreateManyAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await Table.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Deleted)
            {
                Table.Attach(entity);
            }

            await Task.Run(() => Table.Remove(entity));
        }

        public virtual async Task DeleteAsync(TPrimarykey id, CancellationToken cancellationToken = default)
        {
            TEntity? entity = await Table.FindAsync(id, cancellationToken);

            if (entity == null)
            {
                throw new Exception($"Not Found entity id {id}");
            }

            await DeleteAsync(entity);
        }

        public virtual async Task<TEntity[]> DeleteManyAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            var entitiesDelete = Table.Where(p => entities.Select(arg => arg.Id).Contains(p.Id));

            foreach (TEntity entity in entitiesDelete)
            {
                if (context.Entry(entity).State == EntityState.Deleted)
                {
                    Table.Attach(entity);
                }
            }

            await Task.Run(() => Table.RemoveRange(entitiesDelete));

            return await entitiesDelete.ToArrayAsync(cancellationToken);
        }

        public virtual async Task<TEntity[]> DeleteManyAsync(TPrimarykey[] entitieIds, CancellationToken cancellationToken = default)
        {
            var entities = await Table.Where(p => entitieIds.Contains(p.Id)).ToArrayAsync();

            return await DeleteManyAsync(entities, cancellationToken);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetAllQuery().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Table.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Table.ToArrayAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = GetAllQuery();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToArrayAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> GetAllQuery()
        {
            return Table.AsQueryable();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TPrimarykey id, CancellationToken cancellationToken = default)
        {
            return await Table.FindAsync(id, cancellationToken);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity? entityUpdate = await Table.FindAsync(entity.Id, cancellationToken);

            if (entityUpdate == null)
            {
                throw new Exception($"Not Found entity id {entity.Id}");
            }

            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<TEntity[]> UpdateManyAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            var entityUpdate = Table.Where(p => entities.Select(arg => arg.Id).Contains(p.Id));

            context.Entry(entityUpdate).State = EntityState.Modified;

            return await entityUpdate.ToArrayAsync(cancellationToken);
        }
    }
}
