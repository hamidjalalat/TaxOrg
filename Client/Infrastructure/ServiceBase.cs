using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure
{
    public abstract class ServiceBase : object
    {
        // ********************
        #region Properties

        protected string BaseUrl { get; set; }
        protected System.Net.Http.HttpClient Http { get; }
        protected Client.Services.LogsService LogsService { get; }

        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;
        private readonly Infrastructure.HttpInterceptorService _httpInterceptor;

        // ********************
        #endregion
        public ServiceBase
            (
                System.Net.Http.HttpClient http,
                Client.Services.LogsService logsService,
                Microsoft.Extensions.Configuration.IConfiguration configuration,
                Client.Services.Tokens.ITokensService tokenService,
                NazmAuthenticationStateProvider authStateProvider,
                HttpInterceptorService httpInterceptor = null
            ) : base()
        {
            Http = http;
            LogsService = logsService;

            _configuration = configuration;
            _tokenService = tokenService;
            _authStateProvider = authStateProvider;
            _httpInterceptor = httpInterceptor;

            BaseUrl = Utility.getBaseUrl(configuration);
        }

        private async System.Threading.Tasks.Task AddTokenToRequestHeader(bool isAddTokenToHeader)
        {
            if (isAddTokenToHeader)
            {
                //var authState = await _authStateProvider.GetAuthenticationStateAsync();
                //var user = authState.User;
                //var exp = user.FindFirst(c => c.Type.Equals("exp")).Value;
                //var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
                //var timeUTC = DateTime.UtcNow;
                //var diff = expTime - timeUTC;
                //if (diff.TotalMinutes <= 60)

                var tokenResult = await _tokenService.GetToken();
                if (tokenResult != null)
                    Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: Infrastructure.Utility.Key_AuthenticationHeaderScheme, $"{tokenResult.Token}");
            }
        }

        private async System.Threading.Tasks.Task AddRefreshTokenMonitorEvent()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                if (_httpInterceptor != null)
                    _httpInterceptor.MonitorEvent();
            });
            
        }

        // ********************
        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            GetAsync<TResponse>(string url, string query = null, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}";
                if (string.IsNullOrWhiteSpace(query) == false)
                {
                    requestUri = $"{requestUri}?{query}";
                }

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await
                    Http.GetAsync
                    (requestUri: requestUri)
                    ;
                string strTypeResponse = response.GetType().ToString();

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        strTypeResponse = typeof(TResponse).ToString();

                        ////ReadFromJsonAsync->Extension Method-> using System.Net.Http.Json;
                        //TResponse result =
                        //	await
                        //	response.Content.ReadFromJsonAsync<TResponse>();

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        TResponse result = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

                        return result;
                    }

                    // When content type is not valid
                    catch (System.NotSupportedException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - The content type is not supported.";

                        // Static داخل تابع غیر
                        LogsService.AddLog(type: GetType(), message: errorMessage);

                        // Static داخل تابع
                        //LogsService.AddLog(type: typeof(ServiceBase), message: errorMessage);
                    }

                    // Invalid JSON
                    catch (System.Text.Json.JsonException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - Invalid JSON.";

                        LogsService.AddLog(type: GetType(), message: errorMessage);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                string errorMessage =
                    $"Exception: {ex.Message}";

                LogsService.AddLog(type: GetType(), message: errorMessage);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            GetByIdAsync<TResponse>(string url, int id, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}/{id}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await
                    Http.GetAsync
                    (requestUri: requestUri)
                    ;
                string strTypeResponse = response.GetType().ToString();

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        strTypeResponse = typeof(TResponse).ToString();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true,
                        };

                        //ReadFromJsonAsync->Extension Method-> using System.Net.Http.Json;
                        TResponse result =
                            await
                            response.Content.ReadFromJsonAsync<TResponse>();
                        return result;
                    }

                    // When content type is not valid
                    catch (System.NotSupportedException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - The content type is not supported.";

                        // Static داخل تابع غیر
                        LogsService.AddLog(type: GetType(), message: errorMessage);

                        // Static داخل تابع
                        //LogsService.AddLog(type: typeof(ServiceBase), message: errorMessage);
                    }

                    // Invalid JSON
                    catch (System.Text.Json.JsonException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - Invalid JSON.";

                        LogsService.AddLog(type: GetType(), message: errorMessage);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                string errorMessage =
                    $"Exception: {ex.Message}";

                LogsService.AddLog(type: GetType(), message: errorMessage);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            FindByIdAsync<TResponse>(string url, int id, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}/{id}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await
                    Http.GetAsync
                    (requestUri: requestUri)
                    ;
                string strTypeResponse = response.GetType().ToString();

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        strTypeResponse = typeof(TResponse).ToString();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true,
                        };

                        //ReadFromJsonAsync->Extension Method-> using System.Net.Http.Json;
                        TResponse result =
                            await
                            response.Content.ReadFromJsonAsync<TResponse>();
                        return result;
                    }

                    // When content type is not valid
                    catch (System.NotSupportedException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - The content type is not supported.";

                        // Static داخل تابع غیر
                        LogsService.AddLog(type: GetType(), message: errorMessage);

                        // Static داخل تابع
                        //LogsService.AddLog(type: typeof(ServiceBase), message: errorMessage);
                    }

                    // Invalid JSON
                    catch (System.Text.Json.JsonException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - Invalid JSON.";

                        LogsService.AddLog(type: GetType(), message: errorMessage);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                string errorMessage =
                    $"Exception: {ex.Message}";

                LogsService.AddLog(type: GetType(), message: errorMessage);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            FindByIdAsync<TResponse>(string url, System.Guid id, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}/{id}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await
                    Http.GetAsync
                    (requestUri: requestUri)
                    ;
                string strTypeResponse = response.GetType().ToString();

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        strTypeResponse = typeof(TResponse).ToString();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            WriteIndented = true,
                        };

                        //ReadFromJsonAsync->Extension Method-> using System.Net.Http.Json;
                        TResponse result =
                            await
                            response.Content.ReadFromJsonAsync<TResponse>();
                        return result;
                    }

                    // When content type is not valid
                    catch (System.NotSupportedException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - The content type is not supported.";

                        // Static داخل تابع غیر
                        LogsService.AddLog(type: GetType(), message: errorMessage);

                        // Static داخل تابع
                        //LogsService.AddLog(type: typeof(ServiceBase), message: errorMessage);
                    }

                    // Invalid JSON
                    catch (System.Text.Json.JsonException ex)
                    {
                        string errorMessage =
                            $"Exception: {ex.Message} - Invalid JSON.";

                        LogsService.AddLog(type: GetType(), message: errorMessage);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                string errorMessage =
                    $"Exception: {ex.Message}";

                LogsService.AddLog(type: GetType(), message: errorMessage);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }


        // ********************
        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            PostAsync<TRequest, TResponse>(string url, TRequest viewModel, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await Http.PostAsJsonAsync
                    (requestUri: requestUri, value: viewModel);

                string strTypeResponse = response.GetType().ToString();

                var ensureSuccessStatusCode = response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        strTypeResponse = typeof(TResponse).ToString();
                        string strTypeRequest = typeof(TRequest).ToString();

                        //System.Text.Json.JsonSerializerOptions jsonSerializerOptions =
                        //    new System.Text.Json.JsonSerializerOptions
                        //    {
                        //        MaxDepth = 5,
                        //    };

                        //TResponse result =
                        //    await response.Content.ReadFromJsonAsync<TResponse>(options: jsonSerializerOptions);



                        // New Solution
                        TResponse result =
                            await response.Content.ReadFromJsonAsync<TResponse>();
                        return result;
                        // /New Solution

                        // Old Solution
                        //string data =
                        //    await response.Content.ReadAsStringAsync();

                        //TResponse result =
                        //    System.Text.Json.JsonSerializer.Deserialize<TResponse>(data);

                        //var data =
                        //    await response.Content.ReadAsStreamAsync();

                        //TResponse result =
                        //    await System.Text.Json.JsonSerializer.DeserializeAsync<TResponse>(data);

                        //var data =
                        //    await response.Content.ReadAsStringAsync();

                        //TResponse result =
                        //    await response.Content.ReadFromJsonAsync<TResponse>();
                        //return result;
                        // /Old Solution
                    }
                    // When content type is not valid
                    catch (System.NotSupportedException)
                    {
                        System.Console.WriteLine("The content type is not supported.");
                    }
                    // Invalid JSON
                    catch (System.Text.Json.JsonException)
                    {
                        System.Console.WriteLine("Invalid JSON.");
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Dispose();
                //response = null;
            }

            return default;
        }

        // ********************
        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            PostUploadAsync<TResponse>(string url, HttpContent httpContent, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await Http.PostAsync
                    (requestUri: requestUri, content: httpContent);

                string strTypeResponse = response.GetType().ToString();

                var ensureSuccessStatusCode = response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        // New Solution
                        TResponse result =
                            await response.Content.ReadFromJsonAsync<TResponse>();
                        return result;
                    }
                    // When content type is not valid
                    catch (System.NotSupportedException)
                    {
                        System.Console.WriteLine("The content type is not supported.");
                    }
                    // Invalid JSON
                    catch (System.Text.Json.JsonException)
                    {
                        System.Console.WriteLine("Invalid JSON.");
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Dispose();
                //response = null;
            }

            return default;
        }


        // ********************
        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            PutAsync<TRequest, TResponse>(string url, TRequest viewModel, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {

                string requestUri = $"{BaseUrl}/{url}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await Http.PutAsJsonAsync
                    (requestUri: requestUri, value: viewModel);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        TResponse result =
                            await response.Content.ReadFromJsonAsync<TResponse>();

                        return result;
                    }
                    // When content type is not valid
                    catch (System.NotSupportedException)
                    {
                        System.Console.WriteLine("The content type is not supported.");
                    }
                    // Invalid JSON
                    catch (System.Text.Json.JsonException)
                    {
                        System.Console.WriteLine("Invalid JSON.");
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }


        // ********************
        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            DeleteAsync<TResponse>(string url, int id, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}{id}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await Http.DeleteAsync(requestUri: requestUri);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        TResponse result =
                            await response.Content.ReadFromJsonAsync<TResponse>();

                        return result;
                    }
                    // When content type is not valid
                    catch (System.NotSupportedException)
                    {
                        System.Console.WriteLine("The content type is not supported.");
                    }
                    // Invalid JSON
                    catch (System.Text.Json.JsonException)
                    {
                        System.Console.WriteLine("Invalid JSON.");
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual
            async System.Threading.Tasks.Task<TResponse>
            DeleteAsync<TResponse>(string url, System.Guid id, bool isAddTokenToHeader = true)
        {
            System.Net.Http.HttpResponseMessage response = null;

            try
            {
                string requestUri = $"{BaseUrl}/{url}{id}";

                await AddTokenToRequestHeader(isAddTokenToHeader);
                await AddRefreshTokenMonitorEvent();

                response =
                    await Http.DeleteAsync(requestUri: requestUri);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        TResponse result =
                            await response.Content.ReadFromJsonAsync<TResponse>();

                        return result;
                    }
                    // When content type is not valid
                    catch (System.NotSupportedException)
                    {
                        System.Console.WriteLine("The content type is not supported.");
                    }
                    // Invalid JSON
                    catch (System.Text.Json.JsonException)
                    {
                        System.Console.WriteLine("Invalid JSON.");
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }
    }
}
