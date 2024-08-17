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

    public class UserForgotPasswordCommand : IRequest<Result<bool>>
    {
        private UserForgotPasswordCommand()
        {

        }
        public UserForgotPasswordCommand(UserAuthenticationForgotPasswordViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserAuthenticationForgotPasswordViewModel InputViewModel { get; private set; }
    }

    public class UserForgotPasswordCommandHandler : BaseRequestHandler<UserForgotPasswordCommand, Result<bool>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IMessageSender _messageSender;
        private readonly IOptions<PhoneTotpOptions> _phoneTotpOptions;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IOptions<ApiSettings> _apiSetting;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        private readonly IOptions<EmailConfig> _emailConfig;

        public UserForgotPasswordCommandHandler(IUnitOfWork unitOfWork,
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

        protected override async Task<Result<bool>> HandleRequestAsync(UserForgotPasswordCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();

            var user = await _userManager.Users.Where(s => s.Email == input.InputViewModel.Email).FirstOrDefaultAsync(cancellationToken);
            if (user == null) throw new Exception(Resources.Messages.Errors.InvalidData);
            //if (user.EmailConfirmed == false)
            //{
            //    return Ok(new Response<ForgotPasswordViewModel>(model, new List<string>() { "ایمیل شما برای ریست کردن رمز تایید نشده است " }));
            //}
            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordUrl = _apiSetting.Value.ResetPasswordUrl + "?email=" + user.Email + "&token=" + resetPasswordToken;

            await _messageSender.SendEmail(new string[] { user.Email }, "reset password link", resetPasswordUrl, _emailConfig.Value);
            return response
                .WithSuccess(Resources.Messages.Successes.ForgotPassword)
                .ConvertToDtatResult();
        }
    }
}
