using Mask.Application.Interfaces;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mask.Application.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IGenericRepository<ProductType, Guid, string> _productTypeRepository;

        public ProductTypeService(IGenericRepository<ProductType, Guid, string> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ProductType> CreateProductTypeAsync(ProductType productType)
        {
            var isExisted = await _productTypeRepository.GetAllQuery()
                .AnyAsync(p => p.Name == productType.Name && p.Version > productType.Version);

            throw new ApplicationException($"Created {nameof(ProductType)} failed!");
        }
    }
}
