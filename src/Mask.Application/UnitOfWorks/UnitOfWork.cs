﻿using Mask.Infrastructure.DbContexts;

namespace Mask.Application.UnitOfWorks
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private MaskDbContext _dbContext;

        public UnitOfWork(MaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
