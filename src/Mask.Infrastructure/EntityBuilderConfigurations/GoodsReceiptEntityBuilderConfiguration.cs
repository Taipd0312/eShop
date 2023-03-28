using Mask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mask.Infrastructure.EntityBuilderConfigurations
{
    internal class GoodsReceiptEntityBuilderConfiguration : IEntityTypeConfiguration<GoodsReceipt>
    {
        public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
