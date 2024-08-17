using Infrastructure;
using System;
using System.Security.Claims;
using static MudBlazor.CategoryTypes;

namespace Client.Services
{
    public class UserAuthenticationsService : Infrastructure.ServiceBase
    {
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public UserAuthenticationsService
            (
                System.Net.Http.HttpClient httpClient,
                LogsService logsService,
                Microsoft.Extensions.Configuration.IConfiguration configuration,
                Client.Services.Tokens.ITokensService tokenService,
                Infrastructure.NazmAuthenticationStateProvider authStateProvider
            ) : base(httpClient, logsService, configuration, tokenService, authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _tokenService = tokenService;

            //BaseUrl = Infrastructure.Utility.getBaseUrl(configuration);
            strServiceUri = $"{Infrastructure.Utility.setServiceUri<UserAuthenticationsService>()}";
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
          SimpleLoginAsync(ViewModels.Users.UserAuthenticationLoginViewModel viewModel)
        {
            try
            {
                string url = $"{strServiceUri}SimpleLogin";

                var result =
                    await
                    PostAsync<ViewModels.Users.UserAuthenticationLoginViewModel,
                    Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
                    (url: url, viewModel);

                return result;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            return default;
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
          LoginAsync(ViewModels.Users.UserAuthenticationLoginViewModel viewModel)
        {
            try
            {
                string url = $"{strServiceUri}Login";

                var result =
                    await
                    PostAsync<ViewModels.Users.UserAuthenticationLoginViewModel,
                    Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
                    (url: url, viewModel, isAddTokenToHeader: false);

                if (result.IsSuccess && result.Value != null)
                {
                    //var securityToken = Infrastructure.JwtParser.ParseClaimsFromJwt(result.Value.Token);
                    //Console.WriteLine($"result: {result.Value.Token}");
                    //foreach (var item in securityToken)
                    //{
                    //	//if(item.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                    //		Console.WriteLine($" {item} ");
                    //}

                    await _tokenService.SetToken(result.Value);

                    var securityToken = JwtParser.ParseClaimsFromJwt(result.Value.Token);
                    foreach (var item in securityToken)
                    {
                        if (item.Type == ClaimTypes.NameIdentifier)
                            await _tokenService.SetUserId(item.Value);
                    }

                    await _authStateProvider.StateChanged();
                }

                return result;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            return default;
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
          RefreshTokenAsync(ViewModels.Users.UserAuthenticationTokenApiViewModel viewModel)
        {
            try
            {
                string url = $"{strServiceUri}RefreshToken";

                var result =
                    await
                    PostAsync<ViewModels.Users.UserAuthenticationTokenApiViewModel,
                    Nazm.Results.Result<ViewModels.Users.UserAuthenticationLoginResultViewModel>>
                    (url: url, viewModel, isAddTokenToHeader: false);

                if (result.IsSuccess && result.Value != null)
                {
                    await _tokenService.SetToken(result.Value);
                    await _authStateProvider.StateChanged();
                }

                return result;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            return default;
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result<bool>>
          LogOutAsync()
        {
            try
            {
                string url = $"{strServiceUri}Logout";

                var result =
                    await
                    PostAsync
                    <ViewModels.Users.UserAuthenticationLoginViewModel,
                    Nazm.Results.Result<bool>>
                    (url: url, null);

                await _tokenService.RemoveToken();
                await _tokenService.RemoveUserId();
                await _authStateProvider.StateChanged();

                return result;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            return default;
        }
    }
}
