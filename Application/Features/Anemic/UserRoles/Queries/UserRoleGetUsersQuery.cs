using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.UserRoles;
using ViewModels.Users;

namespace Application.Features.Anemic.UserRoles.Queries
{

    public class UserRoleGetUsersQuery : IRequest<Result<List<UserManageSimpleViewModel>>>
    {
        public UserRoleGetUsersQuery(UserRoleInputParamsViewModel inputParamsViewModel)
        {
            InputParamsViewModel = inputParamsViewModel;
        }
        public UserRoleInputParamsViewModel InputParamsViewModel { get; private set; }

    }

    public class UserRoleGetUsersQueryHandler : BaseRequestHandler<UserRoleGetUsersQuery, Result<List<UserManageSimpleViewModel>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserRoleGetUsersQueryHandler(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        protected async override Task<Result<List<UserManageSimpleViewModel>>> HandleRequestAsync(UserRoleGetUsersQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<UserManageSimpleViewModel>>();
            var role = await _roleManager.FindByIdAsync(input.InputParamsViewModel.RoleId);

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            var viewModel = _mapper.Map<List<UserManageSimpleViewModel>>(users);
            return result.WithValue(viewModel).ConvertToDtatResult();
        }
    }
}
