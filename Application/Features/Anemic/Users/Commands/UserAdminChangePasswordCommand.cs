using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserAdminChangePasswordCommand : IRequest<Result<bool>>
    {
        public UserManageChangePasswordByAdminViewModel InputViewModel { get; private set; }

        public UserAdminChangePasswordCommand(UserManageChangePasswordByAdminViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
    }

    public class UserAdminChangePasswordCommandHandler : BaseRequestHandler<UserAdminChangePasswordCommand, Result<bool>>
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        public UserAdminChangePasswordCommandHandler(IUnitOfWork unitOfWork,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserAdminChangePasswordCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            List<string> errorList = new List<string>();

            var currentUser = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == input.InputViewModel.UserId);
            var IsRemove = await _userManager.RemovePasswordAsync(currentUser);
            var result = await _userManager.AddPasswordAsync(currentUser, input.InputViewModel.NewPassword);

            if (result == null)
            {
                errorList.Add(Resources.Messages.Errors.UnexpectedError);
            }
            else if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
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
                    .WithSuccess(Resources.Messages.Successes.SuccessUserPasswordChanged)
                    .ConvertToDtatResult();
            }
        }
    }
}
