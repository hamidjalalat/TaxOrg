using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class InvoiceModelConfiguration : IEntityTypeConfiguration<InvoiceModel>
    {
        public void Configure(EntityTypeBuilder<InvoiceModel> builder)
        {
            builder.Property(A => A.Name).HasMaxLength(100);
        }
    }
}
