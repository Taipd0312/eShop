namespace Mask.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string TypeId { get; set; }

        public decimal Cost { get; set; }

        public string? ColorId { get; set; }

        public string? Description { get; set; }

        public List<string>? Images { get; set; }

        public int Version { get; set; }

        public decimal Sell { get; set; }

        public int Total { get; set; }
    }
}
