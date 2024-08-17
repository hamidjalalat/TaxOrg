using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.UserRoles;

namespace Application.Features.Anemic.Users.Commands
{
    public class UserAddToRoleCommand : IRequest<Result<UserRoleCreateViewModel>>
    {
        public UserRoleCreateViewModel ViewModel { get; set; }

    }

    public class UserAddToRoleCommandHandler : BaseRequestHandler<UserAddToRoleCommand, Result<UserRoleCreateViewModel>>
    {
        private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserAddToRoleCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            ) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
			_roleManager = roleManager;
		}

        protected override async Task<Result<UserRoleCreateViewModel>> HandleRequestAsync(UserAddToRoleCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserRoleCreateViewModel>();
            List<string> errorList = new List<string>();
            var user = await _userManager.FindByIdAsync(input.ViewModel.UserId);
			var role = await _roleManager.FindByIdAsync(input.ViewModel.RoleId);
			var roleName = await _roleManager.GetRoleNameAsync(role);
            var userRole=await _userManager.IsInRoleAsync(user, roleName);
            if (!userRole)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    return response
                        .WithValue(input.ViewModel)
                        .ConvertToDtatResult();
                }
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
            }
            else
            {
                errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.UserRole));
            }
            return response
                .WithErrors(errorList)
                .ConvertToDtatResult();
        }
    }
}
