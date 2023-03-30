namespace Mask.Domain.Entities
{
    public class ProductColor : BaseEntity
    {
        public string Name { get; set; }

        public string? CSSName { get; set; }

        public string? HexCode { get; set; }

        public int? DecimalCodeR { get; set; }

        public int? DecimalCodeG { get; set; }

        public int? DecimalCodeB { get; set; }
    }
}
