namespace Mask.Domain.Entities
{
    public class GoodsReceipt : BaseEntity
    {
        public string PacketId { get; set; }

        public string? Description { get; set; }
    }
}
