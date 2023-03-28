using Mask.Domain.Entities;

namespace Mask.Application.Interfaces
{
    public interface IProductTypeService
    {
        public Task<ProductType> CreateProductTypeAsync(ProductType productType);
    }
}
