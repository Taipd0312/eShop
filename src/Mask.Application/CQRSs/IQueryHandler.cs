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
