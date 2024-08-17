using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Menus;

namespace Application.Features.Anemic.Menus.Queries
{

    public class MenuGetByUserIdQuery : BaseRequest, IRequest<Result<List<MenuViewModel>>>
    {


    }

    public class MenuGetByUserIdQueryHandler : BaseRequestHandler<MenuGetByUserIdQuery, Result<List<MenuViewModel>>>
    {
        private readonly IMenuRepository _MenuRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MenuGetByUserIdQueryHandler(
            IMenuRepository MenuRepository,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected async override Task<Result<List<MenuViewModel>>> HandleRequestAsync(MenuGetByUserIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<MenuViewModel>>();

            var menuList = new List<MenuViewModel>();
            var menuItem = new HashSet<string>();

            var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                var claims = await _roleManager.GetClaimsAsync(role);
                menuItem.UnionWith(claims.Select(s => s.Type).ToList());
            }
            foreach (var item in menuItem)
            {
                var menu = await _unitOfWork.Menus.FindByIdAsync(Convert.ToInt32(item), cancellationToken);
                if (menu != null)
                {
                    var menuViewModel = _mapper.Map<MenuViewModel>(menu);

                    if (menuViewModel.IsVisible && menu?.IsDeleted == false)
                        menuList.Add(menuViewModel);
                }
            }
            menuList = menuList.OrderBy(o => o.Sort).ToList();
            return result.WithValue(menuList).ConvertToDtatResult();

        }
    }
}
