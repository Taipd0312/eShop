using Mask.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mask.Infrastructure.UoW
{
    public interface IUnitOfWorkCore<TDbConext> 
        where TDbConext : BaseDbContextOptions<TDbConext>, IDisposable
    {
        public Task SaveChangesAsync();

        public Task BeginTransactionAsync();
    }

    public abstract class UnitOfWorkCore<TDbConext> : IUnitOfWorkCore<TDbConext>
        where TDbConext : BaseDbContextOptions<TDbConext>
    {
        private readonly BaseDbContextOptions<TDbConext> _dbContext;
        private IDbContextTransaction? _trans;

        protected UnitOfWorkCore(BaseDbContextOptions<TDbConext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            if (_trans == null)
            {
                await BeginTransactionAsync();
            }

            try
            {
                await _trans!.CommitAsync();
            }
            catch (Exception)
            {
                await _trans!.RollbackAsync();
                throw;
            }
        }

        public Task BeginTransactionAsync()
        {
            if (_trans == null)
            {
                _trans = _dbContext.Database.BeginTransaction();
            }

            return Task.CompletedTask;
        }
    }
}
