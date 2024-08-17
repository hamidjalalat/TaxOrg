using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.UserRoles;

namespace Application.Features.Anemic.Users.Commands
{
	public class UserDeleteFromRoleCommand : IRequest<Result<UserRoleDeleteViewModel>>
	{
		public UserRoleDeleteViewModel ViewModel { get; set; }

	}

	public class UserDeleteFromRoleCommandHandler : BaseRequestHandler<UserDeleteFromRoleCommand, Result<UserRoleDeleteViewModel>>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserDeleteFromRoleCommandHandler(IUnitOfWork unitOfWork,
			IMapper mapper,
			UserManager<User> userManager,
			 RoleManager<IdentityRole> roleManager
			) : base(unitOfWork, mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		protected override async Task<Result<UserRoleDeleteViewModel>> HandleRequestAsync(UserDeleteFromRoleCommand input, CancellationToken cancellationToken)
		{
			var response = new FluentResults.Result<UserRoleDeleteViewModel>();
			var user = await _userManager.FindByIdAsync(input.ViewModel.UserId);
			var role = await _roleManager.FindByIdAsync(input.ViewModel.RoleId);
			var roleName=await _roleManager.GetRoleNameAsync(role);
			var result = await _userManager.RemoveFromRoleAsync(user, roleName);

			List<string> errorList = new List<string>();
			foreach (var error in result.Errors)
			{
				errorList.Add(error.Description);
			}
            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
			else
			{
				return response
                    .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"))
					.WithValue(input.ViewModel)
					.ConvertToDtatResult();
			}
		}
	}
}
