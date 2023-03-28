using Mask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Mask.Infrastructure.EntityBuilderConfigurations
{
    internal class PacketCheckListEntityBuilderConfiguration : IEntityTypeConfiguration<PacketCheckList>
    {
        internal static readonly ValueConverter<List<string>?, string> Converter
            = new ValueConverter<List<string>?, string>(
                convertToProviderExpression: v => v != null ? JsonSerializer.Serialize<IList<string?>>(v, (JsonSerializerOptions)null) : "{}",
                convertFromProviderExpression: v => v == "{}" ? new List<string>() : JsonSerializer.Deserialize<List<string>?>(v, (JsonSerializerOptions)null));

        public void Configure(EntityTypeBuilder<PacketCheckList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Images)
                .HasColumnType("Nvarchar(max)")
                .HasConversion(Converter);
        }
    }
}
