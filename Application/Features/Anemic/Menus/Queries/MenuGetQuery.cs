using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels.Menus;
using Nazm.Results;
using Domain.Constants;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;

namespace Application.Features.Anemic.Menus.Queries
{

    public class MenuGetQuery : BaseRequest, IRequest<Result<List<MenuViewModel>>>
    {

    }

    public class MenuGetQueryHandler : BaseRequestHandler<MenuGetQuery, Result<List<MenuViewModel>>>
    {
        private readonly IMenuRepository _MenuRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MenuGetQueryHandler(
            IMenuRepository MenuRepository,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected async override Task<Result<List<MenuViewModel>>> HandleRequestAsync(MenuGetQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<MenuViewModel>>();
            var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var menuList = new List<MenuViewModel>();
            if (roles.Any(s => s.ToLower().Contains(PublicConstants.Admin)))
            {
                var menuViewModelList = await _unitOfWork.Menus.GetAll.ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider, cancellationToken).ToListAsync(cancellationToken);
                menuList.AddRange(menuViewModelList);
            }
            foreach (var item in roles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                var claims = await _roleManager.GetClaimsAsync(role);

                foreach (var claim in claims)
                {
                    var menu = await _unitOfWork.Menus.GetChildList(Convert.ToInt32(claim.Type), cancellationToken);
                    var menuViewModelList = _mapper.Map<List<MenuViewModel>>(menu);
                    menuList.AddRange(menuViewModelList);
                }
            }
            return result.WithValue(menuList).ConvertToDtatResult();

        }
    }
}
