using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Client.Services.Tokens;
using Microsoft.AspNetCore.Components.Authorization;

namespace Infrastructure
{
    public class NazmAuthenticationStateProvider : AuthenticationStateProvider
    {
        // ********************
        #region Properties

        private readonly ITokensService _tokenService;
        private readonly System.Net.Http.HttpClient _http;

        // ********************
        #endregion

        public NazmAuthenticationStateProvider(System.Net.Http.HttpClient http, ITokensService tokenService)
        {
            _http = http;
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            var tokenResult = await _tokenService.GetToken();            

            if (tokenResult == null)
                identity = new ClaimsIdentity();
            else if (string.IsNullOrWhiteSpace(tokenResult.Token))
                identity = new ClaimsIdentity();
            else
            {
                var securityToken = JwtParser.ParseClaimsFromJwt(tokenResult.Token);
                identity = new ClaimsIdentity(securityToken, Utility.Key_AuthenticationType);
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task StateChanged()
        {
            await Task.Run(() =>
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            });
        }

    }
}
