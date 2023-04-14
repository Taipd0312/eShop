using Mask.Domain.Entities;
using MediatR;

namespace Mask.Application.CQRSs
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }

    public interface IEntityQuery<out TResponse, TEntity, TPrimaryKey, TForeignKey> : IRequest<TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public interface IEntityQueryHandler<in TQuery, TEntity, TPrimaryKey, TForeignKey, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TEntity : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable
        where TForeignKey : IComparable
    {
    }

    public interface IQueryBus
    {
        Task<TResponse> Send<TResponse>(IQuery<TResponse> command);
    }

    public class QueryBus : IQueryBus
    {
        private readonly IMediator mediator;

        public QueryBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<TResponse> Send<TResponse>(IQuery<TResponse> command)
        {
            return mediator.Send(command);
        }
    }
}
