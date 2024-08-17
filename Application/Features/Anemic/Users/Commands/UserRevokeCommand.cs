using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Nazm.Results;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserRevokeCommand : IRequest<Result<bool>>
    {
        private UserRevokeCommand()
        {

        }
        public UserRevokeCommand(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; private set; }
    }

    public class UserRevokeCommandHandler : BaseRequestHandler<UserRevokeCommand, Result<bool>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        public UserRevokeCommandHandler(IUnitOfWork unitOfWork,
             SignInManager<Domain.Anemic.Entities.User> signInManager,
             IOptions<BearerTokensOptions> bearerTokensOptions,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _signInManager = signInManager;
            _authenticatedUserService = authenticatedUserService;
            _bearerTokensOptions = bearerTokensOptions;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserRevokeCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();

            var user = await _userManager.FindByNameAsync(input.UserId);
            if (user == null) throw new Exception("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            return response
                .WithValue(true)
                .ConvertToDtatResult();
        }
    }
}
