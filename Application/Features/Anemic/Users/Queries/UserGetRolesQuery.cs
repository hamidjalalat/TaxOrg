using Application.Common.Interfaces.Repository.Anemic.Base;
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
using ViewModels.RoleManages;
using ViewModels.UserRoles;

namespace Application.Features.Anemic.Users.Queries
{

    public class UserGetRolesQuery : IRequest<Result<UserRoleViewModel>>
    {
        private UserGetRolesQuery()
        {

        }
        public UserGetRolesQuery(string id)
        {
            UserId = id;
        }
        public string UserId { get; private set; }

    }

    public class UserGetRolesQueryHandler : BaseRequestHandler<UserGetRolesQuery, Result<UserRoleViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserGetRolesQueryHandler(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        protected async override Task<Result<UserRoleViewModel>> HandleRequestAsync(UserGetRolesQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<UserRoleViewModel>();
            var user = await _userManager.FindByIdAsync(input.UserId);
           
            var roles = _roleManager.Roles.ToList();
            var model = new UserRoleViewModel() { UserId = input.UserId };

            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserRoles.Add(new RoleManageViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name
                    });
                }
            }
            var viewModel = _mapper.Map<UserRoleViewModel>(model);
            return result.WithValue(viewModel).ConvertToDtatResult();
        }
    }
}
