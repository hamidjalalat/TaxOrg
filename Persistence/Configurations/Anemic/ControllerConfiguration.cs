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
    public class ControllerConfiguration : IEntityTypeConfiguration<Controller>
    {
        public void Configure(EntityTypeBuilder<Controller> builder)
        {
            builder.ToTable("Controllers", "sec");
        }
    }
}
