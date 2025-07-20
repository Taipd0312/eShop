using Mask.Infrastructure.DbContexts;
using Mask.Infrastructure.UoW;

namespace Mask.Application.UnitOfWorks
{
    public interface IUnitOfWork : IUnitOfWorkCore<MaskDbContext>
    {
    }
}
