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
    public class ActionMethodConfiguration : IEntityTypeConfiguration<ActionMethod>
    {
        public void Configure(EntityTypeBuilder<ActionMethod> builder)
        {
            builder.ToTable("ActionMethods", "sec");
        }
    }
}
