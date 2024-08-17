using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MenuControllers;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IMenuControllerRepository : IRepository<MenuController>
    {
        Task<FluentResults.Result<MenuControllerViewModel>> IsValid(MenuControllerViewModel model,CancellationToken cancellationToken);
    }
}
