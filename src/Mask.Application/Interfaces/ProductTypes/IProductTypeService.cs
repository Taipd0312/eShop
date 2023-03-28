using Mask.Domain.Entities;

namespace Mask.Application.Interfaces.ProductTypes
{
    public interface IProductTypeService : IMaskService
    {
        public Task<ProductType> CreateProductTypeAsync(ProductType productType);
    }
}
