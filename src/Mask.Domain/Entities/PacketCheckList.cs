namespace Mask.Domain.Entities
{
    public class PacketCheckList : BaseEntity
    {
        public string ProductId { get; set; }

        public string PacketId { get; set; }

        public string? Description { get; set; }

        public List<string>? Images { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalPrices { get; set; }
    }
}
