using Mask.Application.CQRSs;
using Mask.Application.Infrastrucetures;
using Mask.Application.Interfaces;
using Mask.Domain.Entities;

namespace Mask.Application.Queries
{
    // Core
    public abstract class MaskCoreEntityQuery<TEntity, TPrimaryKey, TForeignKey, TResponse> : IEntityQuery<TResponse, TEntity, TPrimaryKey, TForeignKey>, IQuery<TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public abstract class MaskCoreEntityQueryHandler<TQuery, TEntity, TPrimaryKey, TForeignKey, TResponse> : IEntityQueryHandler<TQuery, TEntity, TPrimaryKey, TForeignKey, TResponse>
        where TQuery : MaskCoreEntityQuery<TEntity, TPrimaryKey, TForeignKey, TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public readonly IMaskService<TEntity, TPrimaryKey, TForeignKey> _maskService;

        protected MaskCoreEntityQueryHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService)
        {
            _maskService = maskService;
        }

        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }

    //Generic Query all
    public abstract class MaskCoreGetAllEntityQuery<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityQuery<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<IEnumerable<TEntity>>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public abstract class MaskCoreGetAllEntityQueryHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityQueryHandler<MaskCoreGetAllEntityQuery<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<IEnumerable<TEntity>>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreGetAllEntityQueryHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<IEnumerable<TEntity>>> Handle(MaskCoreGetAllEntityQuery<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            return new MaskApplicationResponse<IEnumerable<TEntity>>(await _maskService.GetEntitiesAsync(cancellationToken));
        }
    }

    //Generic Query Enity
    public abstract class MaskCoreGetEntityByIdQuery<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityQuery<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public TPrimaryKey? Id { get; set; } = default;
    }

    public abstract class MaskCoreGetByIdHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityQueryHandler<MaskCoreGetEntityByIdQuery<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreGetByIdHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<TEntity>> Handle(MaskCoreGetEntityByIdQuery<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            if (request.Id != null && request != default)
            {
                return new MaskApplicationResponse<TEntity>(await _maskService.GetEntityByIdAsync(request.Id));
            }

            throw new ApplicationException($"Id invalid!");
        }
    }
}
