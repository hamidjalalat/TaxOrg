using Application.Common.Extensions;
using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Controllers;
//using ViewModels.Roles;
using ViewModels.Shared;
using static Application.Common.GridHelper;
using System.Security.Claims;
using ViewModels.RoleMenus;

namespace Application.Features.Anemic.RoleMenus.Queries
{

    public class RoleMenuGetQuery : BaseRequest, IRequest<Result<List<RoleMenuManageSelectedViewModel>>>
    {
        private RoleMenuGetQuery()
        {

        }
        public RoleMenuGetQuery(string roleId)
        {
            RoleId = roleId;
        }
        public string RoleId { get; private set; }
    }

    public class RoleMenuGetQueryHandler : BaseRequestHandler<RoleMenuGetQuery, Result<List<RoleMenuManageSelectedViewModel>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleMenuGetQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<List<RoleMenuManageSelectedViewModel>>> HandleRequestAsync(RoleMenuGetQuery input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<List<RoleMenuManageSelectedViewModel>>();
            var role = await _roleManager.FindByIdAsync(input.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            var menus = await _unitOfWork.Menus.GetAll
                .Where(q => q.MenuId > 1)
                .OrderBy(o => o.Sort)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var result = _mapper.Map<List<RoleMenuManageSelectedViewModel>>(menus);
            foreach (var item in result)
            {
                if (claims.Any(s => s.Type == item.MenuId.ToString()) && item.IsSelected == false)
                {
                    item.IsSelected = true;
                    var childMenu = await _unitOfWork.Menus.FindByIdAsync(item.MenuId, cancellationToken);
                    var parentMenu = await _unitOfWork.Menus.GetAll
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.ParentId == childMenu.ParentId.GetAncestor(1), cancellationToken);

                    var menu = result.Where(s => s.MenuId == parentMenu?.MenuId).FirstOrDefault();
                    if (menu != null)
                        menu.IsSelected = true;
                }
            }

            return response.WithValue(result).ConvertToDtatResult();
        }
    }
}
