using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using System.Security.Authentication;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserLogoutCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }

    }

    public class UserLogoutCommandHandler : BaseRequestHandler<UserLogoutCommand, Result<bool>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;

        public UserLogoutCommandHandler(IUnitOfWork unitOfWork,
             SignInManager<Domain.Anemic.Entities.User> signInManager,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _signInManager = signInManager;
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserLogoutCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();

            var user = await _userManager.FindByIdAsync(input.UserId);
            if (user == null) throw new AuthenticationException("Invalid user name");

            user.RefreshToken = null;
            user.SecurityStamp=Guid.NewGuid().ToString();
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            _unitOfWork.LoginHistories.Insert(new LoginHistory
            {
                UserId = input.UserId,
                IPAddress = _authenticatedUserService.IPAddress,
                ComputerName = _authenticatedUserService.ComputerName,
                HistoryType = Domain.Enums.HistoryTypeEnum.Logout
            });
            await _unitOfWork.Commit(cancellationToken);
            await _signInManager.SignOutAsync();

            return response
                .WithValue(true)
                .ConvertToDtatResult();
        }
    }
}
