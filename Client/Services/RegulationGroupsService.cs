
namespace Client.Services
{
	public class RegulationGroupsService : Infrastructure.ServiceBase
	{
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public RegulationGroupsService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<RegulationGroupsService>()}";
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.RegulationGroups.RegulationGroupViewModel>>>
			FetchAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchAll";

			var result =
				await
				PostAsync
				<ViewModels.Shared.PublicViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.RegulationGroups.RegulationGroupViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}



        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <byte[]>>
            FetchAllAndFileDownloadAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchAll";

            var result =
                await
                PostAsync
                <ViewModels.Shared.PublicViewModel,
                Nazm.Results.Result<byte[]>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
		   System.Threading.Tasks.Task
		   <ViewModels.RegulationGroups.RegulationGroupViewModel>
		   FindByIdAsync(int Id)
		{
			string url = $"{strServiceUri}FindById";

			var result =
				await
				FindByIdAsync
				<Nazm.Results.Result
				<ViewModels.RegulationGroups.RegulationGroupViewModel>>
				(url: url, Id);

			return result.Value;
		}

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <ViewModels.PagingViewModel
            <ViewModels.RegulationGroups.RegulationGroupActiveViewModel>>>
			FetchActiveAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchActive";

            var result =
                await
                PostAsync
                <ViewModels.Shared.PublicViewModel,
                Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.RegulationGroups.RegulationGroupActiveViewModel>>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        //public
        //	async
        //	System.Threading.Tasks.Task
        //	<Nazm.Results.Result
        //	<ViewModels.PagingViewModel
        //	<ViewModels.RegulationGroups.RegulationGroupActiveViewModel>>>
        //	GetActiveAsync(int pageNumber = Infrastructure.Utility.PageNumber, int pageSize = Infrastructure.Utility.PageSize)
        //{
        //	string url = $"{strServiceUri}GetActive";
        //	string query = $"pageNumber={pageNumber}&pageSize={pageSize}";

        //	var result =
        //		await
        //		GetAsync
        //		<Nazm.Results.Result
        //		<ViewModels.PagingViewModel
        //		<ViewModels.RegulationGroups.RegulationGroupActiveViewModel>>>
        //		(url: url, query: query);

        //	return result;
        //}

        public async
		  System.Threading.Tasks.Task
		  <Nazm.Results.Result
		  <ViewModels.RegulationGroups.RegulationGroupViewModel>>
		  PostAsync(ViewModels.RegulationGroups.RegulationGroupCreateViewModel viewModel)
		{
			try
			{
				string url = $"{strServiceUri}";

				var result =
					await
					PostAsync
					<ViewModels.RegulationGroups.RegulationGroupCreateViewModel,
					Nazm.Results.Result<ViewModels.RegulationGroups.RegulationGroupViewModel>>
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
		<Nazm.Results.Result
		<ViewModels.RegulationGroups.RegulationGroupViewModel>>
		PutAsync(ViewModels.RegulationGroups.RegulationGroupUpdateViewModel viewModel)
		{
			try
			{
				string url = $"{strServiceUri}";

				var result =
					await
					PutAsync
					<ViewModels.RegulationGroups.RegulationGroupUpdateViewModel,
					Nazm.Results.Result<ViewModels.RegulationGroups.RegulationGroupViewModel>>
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
			<Nazm.Results.Result>
		  DeleteAsync(int Id)
		{
			string url = $"{strServiceUri}";

			var result =
				await
				DeleteAsync<Nazm.Results.Result>
				(url: url, Id);

			return result;

		}


	}
}
