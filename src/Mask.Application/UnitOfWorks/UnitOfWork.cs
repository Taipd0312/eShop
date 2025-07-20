using Mask.Infrastructure.DbContexts;
using Mask.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

namespace Mask.Application.UnitOfWorks
{
    public sealed class UnitOfWork : UnitOfWorkCore<MaskDbContext>, IUnitOfWork
    {
        public UnitOfWork(MaskDbContext dbContext) : base(dbContext)
        {
        }
    }
}
