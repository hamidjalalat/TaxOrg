using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using Application.Common.Security.PhoneTotp;
using Application.Features.Anemic.Users.Commands;
using Application.Features.Anemic.Users.Queries;
using AutoMapper;
using Domain.Anemic.Entities;
using Dtat.Security.Jwt;
using Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nazm.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ViewModels.Users;
using User = Domain.Anemic.Entities.User;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthenticationsController : ControllerBase
    {
        private ISender _mediator = null!;
        private IMediator? _publisher = null;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        protected IMediator Publisher => _publisher ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMessageSender _messageSender;
        private readonly IPhoneTotpProvider _phoneTotpProvider;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        private readonly IOptions<EmailConfig> _emailConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IOptions<ApiSettings> _apiSetting;
        private readonly PhoneTotpOptions _phoneTotpOptions;

        public UserAuthenticationsController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMessageSender messageSender,
            IPhoneTotpProvider phoneTotpProvider,
            IOptions<PhoneTotpOptions> phoneTotpOptions,
            IOptions<BearerTokensOptions> bearerTokensOptions,
            IOptions<EmailConfig> emailConfig,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService,
            IOptions<ApiSettings> apiSetting)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageSender = messageSender;
            _phoneTotpProvider = phoneTotpProvider;
            _bearerTokensOptions = bearerTokensOptions;
            _emailConfig = emailConfig;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
            _apiSetting = apiSetting;
            _phoneTotpOptions = phoneTotpOptions?.Value ?? new PhoneTotpOptions();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserAuthenticationLoginViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            var result = await Mediator.Send(new UserLoginQuery(model), cancellationToken);
            return Ok(result);

        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut( CancellationToken cancellationToken)
        {
            var userId = _authenticatedUserService.UserId;
            var result = await Mediator.Send(new UserLogoutCommand { UserId = userId },cancellationToken);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(UserAuthenticationTokenApiViewModel tokenModel, CancellationToken cancellationToken)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await Mediator.Send(new UserRefreshTokenCommand(tokenModel), cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string? username, CancellationToken cancellationToken)
        {
            if (username == null)
                username = User.Identity.Name;
            var result = await Mediator.Send(new UserRevokeCommand(username), cancellationToken);
            return Ok(result);

        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserAuthenticationForgotPasswordViewModel model, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UserForgotPasswordCommand(model), cancellationToken);
            return Ok(result);
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserAuthenticationResetPasswordViewModel model, CancellationToken cancellationToken)
        {
            var fluentResult = new FluentResults.Result<UserAuthenticationResetPasswordViewModel>();
            if (!ModelState.IsValid)
            {
                return Ok(fluentResult.WithError("اطلاعات را به درستی تکمیل نمایید"));
            }
            var result = await Mediator.Send(new UserResetPasswordCommand(model), cancellationToken);
            return Ok(result);
        }


        [HttpPost("SendTotpCode")]
        public async Task<IActionResult> SendTotpCode(UserAuthenticationSendTotpCodeViewModel model, CancellationToken cancellationToken)
        {
            var fluentResult = new FluentResults.Result<UserAuthenticationSendTotpCodeViewModel>();
            if (!ModelState.IsValid)
            {
                return Ok(fluentResult.WithError("اطلاعات به درستی وارد نشده است").ConvertToDtatResult());
            }
            var result = await Mediator.Send(new UserSendTotpCodeCommand(model), cancellationToken);
            return Ok(result);

        }


        [HttpPost("VerifyTotpCode")]
        public async Task<IActionResult> VerifyTotpCode(UserAuthenticationVerifyTotpCodeViewModel model, CancellationToken cancellationToken)
        {
            var fluentResults = new FluentResults.Result<UserAuthenticationVerifyTotpCodeViewModel>();

            if (!ModelState.IsValid)
            {
                return Ok(fluentResults.WithError("اطلاعات به درستی وارد نشده است"));
            }
            var result = await Mediator.Send(new UserVerifyTotpCodeCommand(model),cancellationToken);
            return Ok(result);
        }

    }
}