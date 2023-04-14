using Mask.Application.CQRSs;
using Mask.Application.Infrastrucetures;
using Mask.Application.Interfaces;
using Mask.Domain.Entities;

namespace Mask.Application.Commands
{
    // Core Command
    public abstract class MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, TResponse> : ICommand<TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public abstract class MaskCoreEntityCommandHandler<TCommandIn, TEntity, TPrimaryKey, TForeignKey, TResponse> : ICommandHandler<TCommandIn, TResponse>
        where TCommandIn : MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public readonly IMaskService<TEntity, TPrimaryKey, TForeignKey> _maskService;

        protected MaskCoreEntityCommandHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService)
        {
            _maskService = maskService;
        }

        public abstract Task<TResponse> Handle(TCommandIn request, CancellationToken cancellationToken);
    }

    // Core Create Command
    public abstract class MaskCoreCreateEntityCommand<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public TEntity? Entity { get; set; } = default;
    }

    public abstract class MaskCoreCreateEntityCommandHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommandHandler<MaskCoreCreateEntityCommand<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreCreateEntityCommandHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<TEntity>> Handle(MaskCoreCreateEntityCommand<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            if (request.Entity == null)
            {
                throw new ApplicationException("Can't insert data null!");
            }

            if (request.Entity.Id == null || request.Entity.Id.Equals(default(TPrimaryKey)))
            {
                throw new ApplicationException("The Entity Id invalid");
            }

            await _maskService.CreateEntityAsync(request.Entity, cancellationToken);

            return new MaskApplicationResponse<TEntity>(request.Entity);
        }
    }

    // Core Update Command
    public abstract class MaskCoreUpdateEntityCommand<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public TEntity? Entity { get; set; } = default;
    }

    public abstract class MaskCoreUpdateEntityCommandHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommandHandler<MaskCoreUpdateEntityCommand<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreUpdateEntityCommandHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<TEntity>> Handle(MaskCoreUpdateEntityCommand<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            if (request.Entity == null)
            {
                throw new ApplicationException("Can't update data null!");
            }

            if (request.Entity.Id == null || request.Entity.Id.Equals(default(TPrimaryKey)))
            {
                throw new ApplicationException("The Entity Id invalid");
            }

            await _maskService.UpdateEntityAsync(request.Entity, cancellationToken);

            return new MaskApplicationResponse<TEntity>(request.Entity);
        }
    }

    // Core Delete Command
    public abstract class MaskCoreDeleteEntityCommand<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public TEntity? Entity { get; set; } = default;
    }

    public abstract class MaskCoreDeleteEntityCommandHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommandHandler<MaskCoreDeleteEntityCommand<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TEntity>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreDeleteEntityCommandHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<TEntity>> Handle(MaskCoreDeleteEntityCommand<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            if (request.Entity == null)
            {
                throw new ApplicationException("Can't update data null!");
            }

            if (request.Entity.Id == null || request.Entity.Id.Equals(default(TPrimaryKey)))
            {
                throw new ApplicationException("The Entity Id invalid");
            }

            await _maskService.DeleteEntityAsync(request.Entity);

            return new MaskApplicationResponse<TEntity>(request.Entity);
        }
    }

    // Core Delete By Id Command
    public abstract class MaskCoreDeleteEntityByIdCommand<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommand<TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TPrimaryKey>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        public TPrimaryKey? Id { get; set; } = default;
    }

    public abstract class MaskCoreDeleteEntityByIdCommandHandler<TEntity, TPrimaryKey, TForeignKey> : MaskCoreEntityCommandHandler<MaskCoreDeleteEntityByIdCommand<TEntity, TPrimaryKey, TForeignKey>, TEntity, TPrimaryKey, TForeignKey, MaskApplicationResponse<TPrimaryKey>>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
        protected MaskCoreDeleteEntityByIdCommandHandler(IMaskService<TEntity, TPrimaryKey, TForeignKey> maskService) : base(maskService)
        {
        }

        public override async Task<MaskApplicationResponse<TPrimaryKey>> Handle(MaskCoreDeleteEntityByIdCommand<TEntity, TPrimaryKey, TForeignKey> request, CancellationToken cancellationToken)
        {
            if (request.Id != null && !request.Id.Equals(default(TPrimaryKey)))
            {
                await _maskService.DeleteEntityByIdAsync(request.Id, cancellationToken);

                return new MaskApplicationResponse<TPrimaryKey>(request.Id);
            }

            throw new ApplicationException("The Entity Id invalid");
        }
    }
}
