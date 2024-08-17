using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace Infrastructure
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly Client.Services.Tokens.ITokensService _tokensService;
        private readonly Client.Services.UserAuthenticationsService _userAuthenticationsService;

        public HttpInterceptorService
            (
            HttpClientInterceptor interceptor,
            NavigationManager navManager,
            Client.Services.Tokens.ITokensService tokensService,
            Client.Services.UserAuthenticationsService userAuthenticationsService
            )
        {
            _interceptor = interceptor;
            _navManager = navManager;
            _tokensService = tokensService;
            _userAuthenticationsService = userAuthenticationsService;
        }

        public void MonitorEvent() => _interceptor.AfterSend += InterceptResponse;

        private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var responseCode = e.Response.StatusCode;

                switch (responseCode)
                {
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.BadRequest:
                        _navManager.NavigateTo("NotFound");
                        message = "The requested resorce was not found.";
                        break;
                    //case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                        {
                            var tokenResult = _tokensService.GetToken().Result;
                            if (tokenResult != null)
                            {
                                var tokenApi = new ViewModels.Users.UserAuthenticationTokenApiViewModel()
                                {
                                    AccessToken = tokenResult.Token,
                                    RefreshToken = tokenResult.RefreshToken,
                                };

								System.Console.WriteLine("RefreshToken");
								await _userAuthenticationsService.RefreshTokenAsync(tokenApi);
                            }
                            break;
                        }
                    case HttpStatusCode.RedirectKeepVerb:
                        {
                            string uri = $"{Infrastructure.Utility.setUri<Client.Pages.Login>()}";
                            _navManager.NavigateTo(uri: uri, forceLoad: false);
                            break;
                        }
                    default:
                        //_navManager.NavigateTo("ErrorPage");
                        message = "Something went wrong, please contact Administrator";
                        break;
                }
                // throw new HttpResponseException(message);
            }
        }

        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;

    }
}
