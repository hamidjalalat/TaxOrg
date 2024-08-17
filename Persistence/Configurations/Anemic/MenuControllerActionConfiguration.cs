using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.Anemic
{
    public class MenuControllerActionConfiguration : IEntityTypeConfiguration<MenuControllerAction>
    {
        public void Configure(EntityTypeBuilder<MenuControllerAction> builder)
        {
            builder.ToTable("MenuControllerActions", "sec");
            //builder
            //.HasOne(e => e.MenuController)
            //.WithMany(e => e)
            //.OnDelete(DeleteBehavior.SetNull);
            builder
               .HasOne(c => c.MenuController)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
