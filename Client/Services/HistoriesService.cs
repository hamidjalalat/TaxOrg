
namespace Client.Services
{
    public class HistoriesService : Infrastructure.ServiceBase
	{
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public HistoriesService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<HistoriesService>()}";
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.Histories.HistoryViewModel>>>
			FetchAsync(ViewModels.Histories.HistoryInputParamsViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchAll";

			var result =
				await
				PostAsync
				<ViewModels.Histories.HistoryInputParamsViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Histories.HistoryViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}
        
        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <byte[]>>
            FetchAllAndFileDownloadAsync(ViewModels.Histories.HistoryInputParamsViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchAll";

            var result =
                await
                PostAsync
                <ViewModels.Histories.HistoryInputParamsViewModel,
                Nazm.Results.Result<byte[]>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public
			async
			System.Threading.Tasks.Task
			<Nazm.Results.Result<ViewModels.Histories.HistoryChangedViewModel>>
			GetChangedAsync( string model, long id)
		{
			string url = $"{strServiceUri}GetChanged";
			string query = $"model={model}&id={id}";

			var result =
				await
				GetAsync
				<Nazm.Results.Result<ViewModels.Histories.HistoryChangedViewModel>>
				(url: url, query: query);

			return result;
		}
	}
}
