using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using Application.Common.Security.PhoneTotp;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nazm.Results;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserResetPasswordCommand : IRequest<Result<UserAuthenticationResetPasswordViewModel>>
    {
        private UserResetPasswordCommand()
        {

        }
        public UserResetPasswordCommand(UserAuthenticationResetPasswordViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserAuthenticationResetPasswordViewModel InputViewModel { get; private set; }
    }

    public class UserResetPasswordCommandHandler : BaseRequestHandler<UserResetPasswordCommand, Result<UserAuthenticationResetPasswordViewModel>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IMessageSender _messageSender;
        private readonly IOptions<PhoneTotpOptions> _phoneTotpOptions;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IOptions<ApiSettings> _apiSetting;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        private readonly IOptions<EmailConfig> _emailConfig;

        public UserResetPasswordCommandHandler(IUnitOfWork unitOfWork,
             SignInManager<Domain.Anemic.Entities.User> signInManager,
             IMessageSender messageSender,
            IPhoneTotpProvider phoneTotpProvider,
            IOptions<PhoneTotpOptions> phoneTotpOptions,
            IOptions<BearerTokensOptions> bearerTokensOptions,
            IOptions<EmailConfig> emailConfig,
            IAuthenticatedUserService authenticatedUserService,
             IOptions<ApiSettings> apiSetting,
            IMapper mapper,
            UserManager<Domain.Anemic.Entities.User> userManager
            ) : base(unitOfWork, mapper)
        {
            _signInManager = signInManager;
            _messageSender = messageSender;
            _phoneTotpOptions = phoneTotpOptions;
            _authenticatedUserService = authenticatedUserService;
            _apiSetting = apiSetting;
            _bearerTokensOptions = bearerTokensOptions;
            _emailConfig = emailConfig;
            _userManager = userManager;
        }

        protected override async Task<Result<UserAuthenticationResetPasswordViewModel>> HandleRequestAsync(UserResetPasswordCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserAuthenticationResetPasswordViewModel>();

            var user = await _userManager.Users.Where(s => s.Email == input.InputViewModel.Email).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return response
                    .WithError(Resources.Messages.Successes.SuccessUserPasswordChanged)
                    .ConvertToDtatResult();

            var result = await _userManager.ResetPasswordAsync(user, input.InputViewModel.Token, input.InputViewModel.NewPassword);

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
                    .WithSuccess(Resources.Messages.Successes.SuccessUserPasswordChanged)
                    .ConvertToDtatResult();
            }
        }
    }
}
