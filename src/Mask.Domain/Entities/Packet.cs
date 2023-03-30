namespace Mask.Domain.Entities
{
    public class Packet : BaseEntity
    {
        public decimal TotalCost { get; set; }

        public string? Description { get; set; }

        public List<string>? Images { get; set; }
    }
}
