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
    public class MenuControllerConfiguration : IEntityTypeConfiguration<MenuController>
    {
        public void Configure(EntityTypeBuilder<MenuController> builder)
        {
            builder.ToTable("MenuControllers", "sec");
        }
    }
}
