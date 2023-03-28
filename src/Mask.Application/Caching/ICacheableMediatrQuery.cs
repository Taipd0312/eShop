using Mask.Application.CQRSs;
using MediatR;

namespace Mask.Application.Caching
{
    public interface ICachebleMediatrQuery<TResponse> : IRequest<TResponse>
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }

    public interface ICachebleMediatrQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : ICachebleMediatrQuery<TResponse>
    {
    }
}
