using Mask.Domain.Entities;

namespace Mask.Application.Interfaces.ProductTypes
{
    public interface IProductTypeService : IMaskCoreService
    {
        public Task<ProductType> CreateProductTypeAsync(ProductType productType);
    }
}
