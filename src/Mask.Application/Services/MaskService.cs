using Mask.Application.Interfaces;
using Mask.Application.UnitOfWorks;

namespace Mask.Application.Services
{
    public abstract class MaskService : IMaskService
    {
        public readonly IUnitOfWork UOW;

        protected MaskService(IUnitOfWork uOW)
        {
            UOW = uOW;
        }
    }
}
