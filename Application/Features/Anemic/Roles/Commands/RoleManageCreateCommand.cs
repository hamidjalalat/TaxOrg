using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.RoleManages;

namespace Application.Features.Anemic.Roles.Commands
{

    public class RoleManageCreateCommand : BaseRequest, IRequest<Nazm.Results.Result<RoleManageCreateViewModel>>
    {
        public RoleManageCreateViewModel RoleViewModel { get; set; }

    }

    public class RoleCreateCommandHandler : BaseRequestHandler<RoleManageCreateCommand, Nazm.Results.Result<RoleManageCreateViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Nazm.Results.Result<RoleManageCreateViewModel>> HandleRequestAsync(RoleManageCreateCommand input, CancellationToken cancellationToken)
        {
            List<string> errorList = new List<string>();
            var response = new FluentResults.Result<RoleManageCreateViewModel>();
            var roleExist = await _roleManager.RoleExistsAsync(input.RoleViewModel.Name);
            if (!roleExist)
            {
                var role = new IdentityRole();
                role.Name = input.RoleViewModel.Name;
                var roleName = await _roleManager.FindByNameAsync(role.Name);
                if (roleName == null || roleName.Name!=role.Name)
                {
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (roleResult.Succeeded)
                    {
                        return response
                            .WithSuccess(string.Format(Resources.Messages.Successes.SuccessCreate, Resources.DataDictionary.AspNetRole))
                            .ConvertToDtatResult();
                    }
                    foreach (var error in roleResult.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                }
                else
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.RoleTitle));
                }
                return response
                    .WithErrors(errorList)
                    .ConvertToDtatResult();
            }
            else
            {
                return response
                    .WithError(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.RoleTitle))
                    .ConvertToDtatResult();
            }
        }
    }
}
