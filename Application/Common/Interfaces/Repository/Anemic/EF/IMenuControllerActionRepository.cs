using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MenuControllerActions;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IMenuControllerActionRepository : IRepository<MenuControllerAction>
    {
        Task<FluentResults.Result<MenuControllerActionViewModel>> IsValid(MenuControllerActionViewModel model, CancellationToken cancellationToken);
    }
}
