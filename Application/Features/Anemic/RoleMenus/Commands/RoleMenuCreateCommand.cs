using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using System.Security.Claims;
using ViewModels.RoleMenus;

namespace Application.Features.Anemic.RoleMenus.Commands
{

    public class RoleMenuCreateCommand : BaseRequest, IRequest<Result<RoleMenuCreateViewModel>>
    {
        public RoleMenuCreateViewModel RoleViewModel { get; set; }

    }

    public class RoleMenuCreateCommandHandler : BaseRequestHandler<RoleMenuCreateCommand, Result<RoleMenuCreateViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleMenuCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<RoleMenuCreateViewModel>> HandleRequestAsync(RoleMenuCreateCommand input, CancellationToken cancellationToken)
        {
            List<string> errorList = new List<string>();
            var response = new FluentResults.Result<RoleMenuCreateViewModel>();
            var role = await _roleManager.FindByIdAsync(input.RoleViewModel.RoleId);
            foreach (var item in input.RoleViewModel.MenusId)
            {
                var claim = await _roleManager.GetClaimsAsync(role);
                if (claim != null)
                {
                    if (claim.Count == 0 || claim.Any(s => s.Type != item))
                    {
                        var result = await _roleManager.AddClaimAsync(role, new Claim(item, "true"));
                        foreach (var error in result.Errors)
                        {
                            errorList.Add(error.Description);
                        }
                    }
                    else
                    {
                        errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, $"برای نقش {role.Name} منوی {item} قبلا تعریف شده است"));
                    }
                }

            }
            if (errorList.Count == 0)
            {
                return response
                    .WithSuccess(string.Format(Resources.Messages.Successes.SuccessCreate, Resources.DataDictionary.AspNetRoleClaim))
                    .ConvertToDtatResult();
            }
            return response
                .WithErrors(errorList)
                .ConvertToDtatResult();
        }
    }
}
