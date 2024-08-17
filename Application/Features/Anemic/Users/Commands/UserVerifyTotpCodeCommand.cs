using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using Application.Common.Security.PhoneTotp;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nazm.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserVerifyTotpCodeCommand : IRequest<Result<UserAuthenticationLoginResultViewModel>>
    {
        private UserVerifyTotpCodeCommand()
        {

        }
        public UserVerifyTotpCodeCommand(UserAuthenticationVerifyTotpCodeViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserAuthenticationVerifyTotpCodeViewModel InputViewModel { get; private set; }
    }

    public class UserVerifyTotpCodeCommandHandler : BaseRequestHandler<UserVerifyTotpCodeCommand, Result<UserAuthenticationLoginResultViewModel>>
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
        public UserVerifyTotpCodeCommandHandler(IUnitOfWork unitOfWork,
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

        protected override async Task<Result<UserAuthenticationLoginResultViewModel>> HandleRequestAsync(UserVerifyTotpCodeCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserAuthenticationLoginResultViewModel>();
            var user = await _userManager.FindByIdAsync(input.InputViewModel.UserId);
            if (user == null || user.SecretKey == null)
            {
                return response
                            .WithError(Resources.Messages.Errors.Empty)
                            .ConvertToDtatResult();
            }
            if (user.OTPExpirationTime <= DateTime.Now)
            {
                return response
                    .WithError(Resources.Messages.Errors.OTPExpirationTime)
                    .ConvertToDtatResult();
            }

            var result = _phoneTotpProvider.VerifyTotp(user.SecretKey, input.InputViewModel.TotpCode);
            if (result.Succeeded)
            {
                if (user == null)
                {
                    return response
                        .WithError(Resources.Messages.Errors.PhoneNotFound)
                        .ConvertToDtatResult();
                }

                //if (!user.PhoneNumberConfirmed)
                //{
                //    return Ok(new Response<VerifyTotpCodeViewModel>(model, new List<string>() { "شماره موبایل شما تایید نشده است." }));
                //}

                if (!await _userManager.IsLockedOutAsync(user))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name, user.UserName),
                              new Claim(ClaimTypes.NameIdentifier, user.Id),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(ClaimTypes.Surname, user.FirstName + ' ' + user.LastName),
                             new Claim(ClaimTypes.MobilePhone, user.PhoneNumber??""),
                             new Claim(ClaimTypes.Email, user.Email)
                         };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _signInManager.SignInWithClaimsAsync(user, false, authClaims);
                    
                    return response
                        .WithValue(new UserAuthenticationLoginResultViewModel
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Expiration = token.ValidTo
                        })
                        .ConvertToDtatResult();
                }
                return response
                    .WithError(Resources.Messages.Errors.IsLockedUser)
                    .ConvertToDtatResult();
            }

            if (user != null && user.PhoneNumberConfirmed && !await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.AccessFailedAsync(user);
            }

            return response
                .WithError(Resources.Messages.Errors.NotVerifyOtpCode)
                .ConvertToDtatResult();
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Value.Key));

            var token = new JwtSecurityToken(
                issuer: _bearerTokensOptions.Value.Issuer,
                audience: _bearerTokensOptions.Value.Audience,
                expires: DateTime.Now.AddMinutes(_bearerTokensOptions.Value.AccessTokenExpirationMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
