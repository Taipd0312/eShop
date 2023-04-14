using Mask.Application.Interfaces.ProductTypes;
using Mask.Application.UnitOfWorks;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mask.Application.Services.ProductTypes
{
    public class ProductTypeService : MaskCoreService, IProductTypeService
    {
        private readonly IGenericRepository<ProductType, Guid, string> _productTypeRepository;

        public ProductTypeService(IGenericRepository<ProductType, Guid, string> productTypeRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ProductType> CreateProductTypeAsync(ProductType productType)
        {
            var isExisted = await _productTypeRepository.GetAllQuery()
                .AnyAsync(p => p.Name == productType.Name && p.Version > productType.Version);

            if (isExisted)
            {
                throw new ApplicationException($"Created {nameof(ProductType)} failed!");
            }

            await _productTypeRepository.CreateAsync(productType);
            UOW.Commit();

            return productType;
        }
    }
}
