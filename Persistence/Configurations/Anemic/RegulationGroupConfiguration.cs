using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class RegulationGroupConfiguration : IEntityTypeConfiguration<RegulationGroup>
    {
        public void Configure(EntityTypeBuilder<RegulationGroup> builder)
        {
            builder.Property(s => s.Title).IsRequired().HasMaxLength(50);
        }
    }
}
