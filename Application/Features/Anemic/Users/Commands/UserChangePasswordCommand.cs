using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Nazm.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserChangePasswordCommand : IRequest<Result<bool>>
    {
        private UserChangePasswordCommand()
        {

        }
        public UserChangePasswordCommand(UserManageChangePasswordViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserManageChangePasswordViewModel InputViewModel { get; private set; }
    }

    public class UserChangePasswordCommandHandler : BaseRequestHandler<UserChangePasswordCommand, Result<bool>>
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        public UserChangePasswordCommandHandler(IUnitOfWork unitOfWork,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserChangePasswordCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            List<string> errorList = new List<string>();

            var currentUser = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == _authenticatedUserService.UserId);
            var result = await _userManager.ChangePasswordAsync(currentUser, input.InputViewModel.CurrentPassword, input.InputViewModel.NewPassword);

            if (result == null)
            {
                errorList.Add(Resources.Messages.Errors.OldPasswordIsNotCorrect);
            }
            else if(result.Succeeded == false)
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
