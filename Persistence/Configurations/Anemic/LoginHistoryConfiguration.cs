using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> builder)
        {
            builder.Property(s => s.IPAddress).HasMaxLength(50);
            builder.Property(s => s.ComputerName).HasMaxLength(100);
            builder.Property(s => s.LogDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
        }
    }
}
