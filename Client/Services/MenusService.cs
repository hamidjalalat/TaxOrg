
namespace Client.Services
{
	public class MenusService : Infrastructure.ServiceBase
	{
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public MenusService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<MenusService>()}";
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.Menus.MenuViewModel>>>
			FetchAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchAll";

			var result =
				await
				PostAsync
				<ViewModels.Shared.PublicViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Menus.MenuViewModel>>>
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
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.Menus.MenuViewModel>>>
			FetchChildsAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchChilds";

			var result =
				await
				PostAsync
				<ViewModels.Shared.PublicViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Menus.MenuViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

		public async
		   System.Threading.Tasks.Task
		   <ViewModels.Menus.MenuViewModel>
		   FindByIdAsync(int Id)
		{
			string url = $"{strServiceUri}FindById";

			var result =
				await
				FindByIdAsync
				<Nazm.Results.Result
				<ViewModels.Menus.MenuViewModel>>
				(url: url, Id);

			return result.Value;
		}

		public async
		  System.Threading.Tasks.Task
		  <Nazm.Results.Result
		  <ViewModels.Menus.MenuViewModel>>
		  PostAsync(ViewModels.Menus.MenuCreateViewModel viewModel)
		{
			try
			{
				string url = $"{strServiceUri}";

				var result =
					await
					PostAsync
					<ViewModels.Menus.MenuCreateViewModel,
					Nazm.Results.Result<ViewModels.Menus.MenuViewModel>>
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
		<ViewModels.Menus.MenuViewModel>>
		PutAsync(ViewModels.Menus.MenuUpdateViewModel viewModel)
		{
			try
			{
				string url = $"{strServiceUri}";

				var result =
					await
					PutAsync
					<ViewModels.Menus.MenuUpdateViewModel,
					Nazm.Results.Result<ViewModels.Menus.MenuViewModel>>
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
		  DeleteAsync(int Id)
		{
			string url = $"{strServiceUri}";

			var result =
				await
				DeleteAsync<Nazm.Results.Result>
				(url: url, Id);

			return;
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<System.Collections.Generic.IList
            <ViewModels.Menus.MenuViewModel>>>
			GetUserMenusAsync()
		{
			string url = $"{strServiceUri}GetUserMenus";

			var result =
				await
				GetAsync
				<Nazm.Results.Result
				<System.Collections.Generic.IList
                <ViewModels.Menus.MenuViewModel>>>
				(url: url);

			return result;
		}

	}
}
