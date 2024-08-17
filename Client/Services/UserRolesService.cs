
namespace Client.Services
{
	public class UserRolesService : Infrastructure.ServiceBase
	{
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public UserRolesService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<UserRolesService>()}";
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<System.Collections.Generic.IList
			<ViewModels.Users.UserManageSimpleViewModel>>>
			FetchByRoleIdAsync(ViewModels.UserRoles.UserRoleInputParamsViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchByRoleId";

			var result =
				await
				PostAsync
				<ViewModels.UserRoles.UserRoleInputParamsViewModel,
				Nazm.Results.Result<System.Collections.Generic.IList<ViewModels.Users.UserManageSimpleViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<byte[]>>
			FetchByRoleIdAndFileDownloadAsync(ViewModels.UserRoles.UserRoleInputParamsViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}FetchByRoleId";

			var result =
				await
				PostAsync
				<ViewModels.UserRoles.UserRoleInputParamsViewModel,
				Nazm.Results.Result<byte[]>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.UserRoles.UserRoleCreateViewModel>>>
			AddUserToRoleAsync(ViewModels.UserRoles.UserRoleCreateViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}AddUserToRole";

			var result =
				await
				PostAsync
				<ViewModels.UserRoles.UserRoleCreateViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.UserRoles.UserRoleCreateViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

		public async
			System.Threading.Tasks.Task
			<Nazm.Results.Result
			<ViewModels.PagingViewModel
			<ViewModels.UserRoles.UserRoleDeleteViewModel>>>
			DeleteUserFromRoleAsync(ViewModels.UserRoles.UserRoleDeleteViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}DeleteUserFromRole";

			var result =
				await
				PostAsync
				<ViewModels.UserRoles.UserRoleDeleteViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.UserRoles.UserRoleDeleteViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}

		//public async
		//   System.Threading.Tasks.Task
		//	<ViewModels.PagingViewModel
		//   <ViewModels.UserRoles.AspUserRolesViewModel>>
		//   FindByRoleIdAsync(System.Guid Id)
		//{
		//	string url = $"{strServiceUri}FindByRoleId";

		//	var result =
		//		await
		//		FindByIdAsync
		//		<Nazm.Results.Result
		//		<ViewModels.PagingViewModel
		//		<ViewModels.UserRoles.AspUserRolesViewModel>>>
		//		(url: url, Id);

		//	return result.Value;
		//}

	}
}
