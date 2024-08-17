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
    public class UserActiveByAdminCommand : IRequest<Result<bool>>
    {
        public UserManageActiveByAdminViewModel InputViewModel { get; private set; }

        public UserActiveByAdminCommand(UserManageActiveByAdminViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
    }

    public class UserActiveByAdminCommandHandler : BaseRequestHandler<UserActiveByAdminCommand, Result<bool>>
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        public UserActiveByAdminCommandHandler(IUnitOfWork unitOfWork,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserActiveByAdminCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            List<string> errorList = new List<string>();

            var currentUser = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == input.InputViewModel.UserId);

            if (currentUser == null)
            {
                errorList.Add(Resources.Messages.Errors.UnexpectedError);
            }
            else
            {
                currentUser.IsActive = input.InputViewModel.IsActive;
                _unitOfWork.Users.Update(currentUser);
                await _unitOfWork.Commit(cancellationToken);
            }

            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
            else
            {
                string strMsg = (input.InputViewModel.IsActive) ? Resources.DataDictionary.Active : Resources.DataDictionary.InActive;
                return response
                    .WithSuccess(string.Format(Resources.Messages.Successes.Success, strMsg))
                    .ConvertToDtatResult();
            }
        }
    }
}
