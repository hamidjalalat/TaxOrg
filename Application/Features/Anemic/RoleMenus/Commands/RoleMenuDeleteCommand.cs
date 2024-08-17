using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using System.Security.Claims;
using ViewModels.RoleMenus;

namespace Application.Features.Anemic.RoleMenus.Commands
{

    public class RoleMenuDeleteCommand : BaseRequest, IRequest<Result<RoleMenuDeleteViewModel>>
    {
        public RoleMenuDeleteViewModel RoleViewModel { get; set; }

    }

    public class RoleMenuDeleteCommandHandler : BaseRequestHandler<RoleMenuDeleteCommand, Result<RoleMenuDeleteViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleMenuDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<RoleMenuDeleteViewModel>> HandleRequestAsync(RoleMenuDeleteCommand input, CancellationToken cancellationToken)
        {
			List<string> errorList = new List<string>();
			var response = new FluentResults.Result<RoleMenuDeleteViewModel>();
            var role = await _roleManager.FindByIdAsync(input.RoleViewModel.RoleId);
			foreach (var item in input.RoleViewModel.MenusId)
			{
				var result = await _roleManager.RemoveClaimAsync(role, new Claim(item, "true"));
				foreach (var error in result.Errors)
				{
					errorList.Add(error.Description);
				}
			}
			if (errorList.Count==0)
            {
                return response
                    .WithSuccess(string.Format(Resources.Messages.Successes.SuccessDelete, Resources.DataDictionary.AspNetRoleClaim))
                    .ConvertToDtatResult();
            }
            
            return response
                .WithErrors(errorList)
                .ConvertToDtatResult();
        }
    }
}
