using Mask.Domain.Entities;
using Mask.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mask.Infrastructure.EntityBuilderConfigurations
{
    internal class ProductTypeEntityBuilderConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Unit)
                .HasColumnType("varchar(4)")
                .HasConversion(new EnumToStringConverter<ProductUnitEnums>());
        }
    }
}
