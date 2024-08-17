using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IControllerRepository : IRepository<Controller>
    {
    }
}
