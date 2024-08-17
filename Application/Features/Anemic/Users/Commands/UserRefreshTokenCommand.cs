using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nazm.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserRefreshTokenCommand : IRequest<Result<UserAuthenticationLoginResultViewModel>>
    {
        private UserRefreshTokenCommand()
        {

        }
        public UserRefreshTokenCommand(UserAuthenticationTokenApiViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public UserAuthenticationTokenApiViewModel InputViewModel { get; private set; }
    }

    public class UserRefreshTokenCommandHandler : BaseRequestHandler<UserRefreshTokenCommand, Result<UserAuthenticationLoginResultViewModel>>
    {
        private readonly SignInManager<Domain.Anemic.Entities.User> _signInManager;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly UserManager<Domain.Anemic.Entities.User> _userManager;
        private readonly IOptions<BearerTokensOptions> _bearerTokensOptions;
        public UserRefreshTokenCommandHandler(IUnitOfWork unitOfWork,
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

        protected override async Task<Result<UserAuthenticationLoginResultViewModel>> HandleRequestAsync(UserRefreshTokenCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserAuthenticationLoginResultViewModel>();

            string? accessToken = input.InputViewModel.AccessToken;
            string? refreshToken = input.InputViewModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null || principal.Identity == null)
            {
                throw new AuthenticationException("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;


            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new AuthenticationException("Invalid access token or refresh token");
            }

            var newAccessToken = GetToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return response
                .WithValue(new UserAuthenticationLoginResultViewModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    Expiration = newAccessToken.ValidTo,
                    RefreshToken = refreshToken ?? ""
                })
                .ConvertToDtatResult();
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
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
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
