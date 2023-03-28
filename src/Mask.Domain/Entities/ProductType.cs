using Mask.Domain.Enums;

namespace Mask.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }

        public int Version { get; set; }

        public ProductUnitEnums Unit { get; set; }
    }
}
