using Mask.Application.Infrastrucetures;
using Mask.Application.Interfaces;
using Mask.Domain.Entities;

namespace Mask.Application.Queries
{
    public class BaseMaskCoreGetAllEntityQuery<T, TPramaryKey, TForiegnKey> : MaskCoreGetAllEntityQuery<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }

    public class BaseMaskCoreGetAllEntityQueryHandler<T, TPramaryKey, TForiegnKey> : MaskCoreGetAllEntityQueryHandler<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
        public BaseMaskCoreGetAllEntityQueryHandler(IMaskService<T, TPramaryKey, TForiegnKey> maskService) : base(maskService)
        {
        }

        public override Task<MaskApplicationResponse<IEnumerable<T>>> Handle(MaskCoreGetAllEntityQuery<T, TPramaryKey, TForiegnKey> request, CancellationToken cancellationToken)
        {
            return base.Handle(request, cancellationToken);
        }
    }
}
