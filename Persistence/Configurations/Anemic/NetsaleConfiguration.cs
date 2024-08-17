using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class NetsaleConfiguration : IEntityTypeConfiguration<Netsale>
    {
        public void Configure(EntityTypeBuilder<Netsale> builder)
        {
            builder.Property(A => A.Name).HasMaxLength(100);
        }
    }
}
