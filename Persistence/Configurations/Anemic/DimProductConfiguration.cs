using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class DimProductConfiguration : IEntityTypeConfiguration<DimProduct>
    {
        public void Configure(EntityTypeBuilder<DimProduct> builder)
        {
            builder.Property(A => A.Name).HasMaxLength(100);
        }
    }
}
