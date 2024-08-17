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
using System.Security.Cryptography;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserSendTotpCodeCommand : IRequest<Result<UserAuthenticationSendTotpCodeViewModel>>
    {
        private UserSendTotpCodeCommand()
        {

        }
        public UserSendTotpCodeCommand(UserAuthenticationSendTotpCodeViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserAuthenticationSendTotpCodeViewModel InputViewModel { get; private set; }
    }

    public class UserSendTotpCodeCommandHandler : BaseRequestHandler<UserSendTotpCodeCommand, Result<UserAuthenticationSendTotpCodeViewModel>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IMessageSender _messageSender;
        private readonly IOptions<PhoneTotpOptions> _phoneTotpOptions;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IOptions<ApiSettings> _apiSetting;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        private readonly IOptions<EmailConfig> _emailConfig;
        private readonly IPhoneTotpProvider _phoneTotpProvider;
        public UserSendTotpCodeCommandHandler(IUnitOfWork unitOfWork,
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
            _phoneTotpProvider = phoneTotpProvider;
            _phoneTotpOptions = phoneTotpOptions;
            _authenticatedUserService = authenticatedUserService;
            _apiSetting = apiSetting;
            _bearerTokensOptions = bearerTokensOptions;
            _emailConfig = emailConfig;
            _userManager = userManager;
        }

        protected override async Task<Result<UserAuthenticationSendTotpCodeViewModel>> HandleRequestAsync(UserSendTotpCodeCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserAuthenticationSendTotpCodeViewModel>();

            var user = await _userManager.Users.Where(s => s.PhoneNumber == input.InputViewModel.PhoneNumber).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return response
                    .WithError(Resources.Messages.Errors.PhoneNotFound)
                    .ConvertToDtatResult();
            }


            if (user.OTPExpirationTime >= DateTime.Now)
            {
                var differenceInSeconds = (int)(user.OTPExpirationTime - DateTime.Now).TotalSeconds;
                if (differenceInSeconds > 0)
                    return response
                        .WithError(string.Format(Resources.Messages.Successes.WaitingForResendOtpCode, differenceInSeconds))
                        .ConvertToDtatResult();
            }


            byte[] secretKey;

            using (var rng = RandomNumberGenerator.Create())
            {
                secretKey = new byte[32];
                rng.GetBytes(secretKey);
            }
            var totpCode = _phoneTotpProvider.GenerateTotp(secretKey);

            var userExists = await _userManager.Users
                .AnyAsync(user => user.PhoneNumber == input.InputViewModel.PhoneNumber);
            if (userExists && user.PhoneNumber != null)
            {
                //await _ghasedakWebService.SendSmsAsync(user.PhoneNumber, totpCode);
                user.SecretKey = secretKey;
                user.OTPExpirationTime = DateTime.Now.AddSeconds(_phoneTotpOptions.Value.StepInSeconds);
                await _userManager.UpdateAsync(user);
                return response
                    .WithSuccess(Resources.Messages.Successes.SendOtpCode)
                    .ConvertToDtatResult();
            }
            else
                return response
                    .WithError(Resources.Messages.Errors.InvalidData)
                    .ConvertToDtatResult();
        }
    }
}
