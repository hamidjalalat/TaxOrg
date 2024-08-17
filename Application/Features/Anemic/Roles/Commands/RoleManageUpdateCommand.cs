using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.RoleManages;

namespace Application.Features.Anemic.Roles.Commands
{

    public class RoleManageUpdateCommand : BaseRequest, IRequest<Result<RoleManageUpdateViewModel>>
    {
        public RoleManageUpdateViewModel RoleViewModel { get; set; }

    }

    public class RoleUpdateCommandHandler : BaseRequestHandler<RoleManageUpdateCommand, Result<RoleManageUpdateViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<RoleManageUpdateViewModel>> HandleRequestAsync(RoleManageUpdateCommand input, CancellationToken cancellationToken)
        {
            List<string> errorList = new List<string>();
            var response = new FluentResults.Result<RoleManageUpdateViewModel>();

            if (string.IsNullOrEmpty(input.RoleViewModel.Id) || string.IsNullOrEmpty(input.RoleViewModel.Name))
            {
                return response
                         .WithError(Resources.Messages.Errors.Empty)
                         .ConvertToDtatResult();
            }

            var role = await _roleManager.FindByIdAsync(input.RoleViewModel.Id);
            if (role == null)
            {
                return response
                         .WithError(Resources.Messages.Errors.Empty)
                         .ConvertToDtatResult();
            }
            role.Name = input.RoleViewModel.Name;
            var roleName = await _roleManager.FindByNameAsync(role.Name);
            if (roleName == null || roleName.Id != input.RoleViewModel.Id)
            {
                var roleResult = await _roleManager.UpdateAsync(role);

                if (roleResult.Succeeded)
                {
                    return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.SuccessUpdate, Resources.DataDictionary.AspNetRole))
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
    }
}
