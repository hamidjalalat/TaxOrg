using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IQueryable<Menu>?> GetChildList(int menuId,CancellationToken cancellationToken);
        Task<Result<bool>> IsUnique(Menu model,CancellationToken cancellationToken);
    }
}
