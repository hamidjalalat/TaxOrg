
namespace Client.Services
{
	public class LoginHistoriesService : Infrastructure.ServiceBase
	{
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public LoginHistoriesService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<LoginHistoriesService>()}";
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.LoginHistories.LoginHistoryViewModel>>>
			FetchAsync(ViewModels.LoginHistories.LoginHistoryInputParamsViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchAll";

			var result =
				await
				PostAsync
				<ViewModels.LoginHistories.LoginHistoryInputParamsViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.LoginHistories.LoginHistoryViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <byte[]>>
            FetchAllAndFileDownloadAsync(ViewModels.LoginHistories.LoginHistoryInputParamsViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchAll";

            var result =
                await
                PostAsync
                <ViewModels.LoginHistories.LoginHistoryInputParamsViewModel,
                Nazm.Results.Result<byte[]>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }
    }
}
