using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class TspagentConfiguration : IEntityTypeConfiguration<Tspagent>
    {
        public void Configure(EntityTypeBuilder<Tspagent> builder)
        {
            builder.Property(s => s.Address).IsRequired().HasMaxLength(50);
        }
    }
}
