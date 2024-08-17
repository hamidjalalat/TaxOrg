using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Anemic.EF
{
    public class ActionMethodRepository : EFRepository<ActionMethod>, IActionMethodRepository
    {
        public ActionMethodRepository(EFContext context) : base(context)
        {
        }
    }
}
