using Mask.Application.Caching;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mask.Application.Queries.Products
{
    public class GetAllProductsQuery : ICachebleMediatrQuery<IEnumerable<Product>>
    {
        public bool BypassCache { get; set; }

        public string CacheKey { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }
    }

    public class GetAllProductsQueryHandler : ICachebleMediatrQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IGenericRepository<Product, Guid, string> productRepository;

        public GetAllProductsQueryHandler(IGenericRepository<Product, Guid, string> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productQuery = productRepository.GetAllQuery();


            return await productQuery.ToArrayAsync();
        }
    }
}
