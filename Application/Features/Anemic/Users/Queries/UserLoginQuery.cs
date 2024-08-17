using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using Application.Common.Security.PhoneTotp;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nazm.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Queries
{
    public class UserLoginQuery : IRequest<Result<UserAuthenticationLoginResultViewModel>>
    {
        public UserAuthenticationLoginViewModel InputViewModel { get; private set; }

        public UserLoginQuery(UserAuthenticationLoginViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
    }

    public class UserLoginQueryHandler : BaseRequestHandler<UserLoginQuery, Result<UserAuthenticationLoginResultViewModel>>
    {
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
        public UserLoginQueryHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMessageSender messageSender,
            IPhoneTotpProvider phoneTotpProvider,
            IOptions<PhoneTotpOptions> phoneTotpOptions,
            IOptions<BearerTokensOptions> bearerTokensOptions,
            IOptions<EmailConfig> emailConfig,
            IAuthenticatedUserService authenticatedUserService,
            IOptions<ApiSettings> apiSetting,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
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

        protected async override Task<Result<UserAuthenticationLoginResultViewModel>> HandleRequestAsync(UserLoginQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<UserAuthenticationLoginResultViewModel>();

            try
            {
                var user = await _userManager.FindByNameAsync(input.InputViewModel.UserName);
                if (user == null)
                {
                    return result
                        .WithError(Resources.Messages.Errors.InvalidLogin)
                        .ConvertToDtatResult()
                        ;
                }

                if (user.IsActive == false)
                {
                    return result
                        .WithError(Resources.Messages.Errors.AccountInActive)
                        .ConvertToDtatResult()
                        ;
                }
                
                var response = await _signInManager.CheckPasswordSignInAsync(user, input.InputViewModel.Password, true);
                if (response.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName??""),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Surname, user.FirstName + ' ' + user.LastName),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber??""),
                        new Claim(ClaimTypes.Email, user.Email?? ""),
                        new Claim(ClaimTypes.Role,JsonSerializer.Serialize(userRoles),JsonClaimValueTypes.JsonArray)
                    };

                    var token = GetToken(authClaims);
                    if (token == null)
                    {
                        throw new UnauthorizedAccessException("Invalid Token");
                    }

                    if (user.RefreshToken == null || user.RefreshTokenExpiryTime <= DateTime.Now)
                    {
                        user.RefreshToken = GenerateRefreshToken();
                        user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_bearerTokensOptions.Value.RefreshTokenValidityInDays);
                        await _userManager.UpdateAsync(user);
                    }
                    _unitOfWork.LoginHistories.Insert(new LoginHistory
                    {
                        UserId = user.Id,
                        IPAddress = _authenticatedUserService.IPAddress,
                        ComputerName = _authenticatedUserService.ComputerName,
                        HistoryType = Domain.Enums.HistoryTypeEnum.Login
                    });
                    await _unitOfWork.Commit(cancellationToken);

                    return result.WithValue(new UserAuthenticationLoginResultViewModel
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        RefreshToken = user.RefreshToken,
                    }).ConvertToDtatResult();
                }
                if (response.IsLockedOut)
                {
                    return result
                        .WithError(Resources.Messages.Errors.IsLockedOut)
                        .ConvertToDtatResult()
                        ;
                }
                else
                    return result
                        .WithError(Resources.Messages.Errors.InvalidLogin)
                        .ConvertToDtatResult()
                        ;
            }
            catch (Exception ex)
            {
                var error = ex;
                if (error.InnerException != null && error.InnerException.Message.ToLower().Contains("cannot open database"))
                {
                    return result
                        .WithError(Resources.Messages.Errors.ServerNotResponse)
                        .ConvertToDtatResult()
                        ;
                }
                else
                {
                    return result
                        .WithError(ex.Message)
                        .ConvertToDtatResult()
                        ;
                }
                throw;
            }
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokensOptions.Value.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
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