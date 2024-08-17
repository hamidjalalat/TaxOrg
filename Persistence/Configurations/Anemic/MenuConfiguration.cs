using Application.Common.Interfaces.Repository.Anemic;
using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.Anemic
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            
            builder.Property(s => s.Title).IsRequired().HasMaxLength(255);
            builder.Property(s => s.Url).IsRequired().HasMaxLength(255);
            builder.Property(s => s.PageTitle).IsRequired().HasMaxLength(255);
            builder.Property(s => s.MenuIcon).IsRequired().HasMaxLength(255);
            builder.Property(s => s.PageIcon).IsRequired().HasMaxLength(255);
            builder.Property(s => s.PageDescription).HasMaxLength(512);
        }

    }
}
