
using System.Collections.Generic;

namespace Client.Services
{
    public class RoleMenusService : Infrastructure.ServiceBase
    {
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public RoleMenusService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<RoleMenusService>()}";
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <ViewModels.PagingViewModel
            <ViewModels.RoleMenus.RoleMenuCreateViewModel>>>
            AddRoleToMenuAsync(ViewModels.RoleMenus.RoleMenuCreateViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}AddRoleToMenu";

            var result =
                await
                PostAsync
                <ViewModels.RoleMenus.RoleMenuCreateViewModel,
                Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.RoleMenus.RoleMenuCreateViewModel>>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

		public async
	        System.Threading.Tasks.Task
	        <Nazm.Results.Result
	        <ViewModels.PagingViewModel
	        <ViewModels.RoleMenus.RoleMenuDeleteViewModel>>>
			DeleteRoleMenuAsync(ViewModels.RoleMenus.RoleMenuDeleteViewModel inputParamsViewModel)
		{
			string url = $"{strServiceUri}DeleteRoleMenu";

			var result =
				await
				PostAsync
				<ViewModels.RoleMenus.RoleMenuDeleteViewModel,
				Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.RoleMenus.RoleMenuDeleteViewModel>>>
				(url: url, viewModel: inputParamsViewModel);

			return result;
		}


		public async
		   System.Threading.Tasks.Task
		    <Nazm.Results.Result
            <List
		   <ViewModels.RoleMenus.RoleMenuManageSelectedViewModel>>>
		   FetchByRoleIdAsync(System.Guid Id)
		{
			string url = $"{strServiceUri}FetchByRoleId";
			string query = $"roleId={Id}";

			var result =
				await
				GetAsync
				<Nazm.Results.Result
                <List
				<ViewModels.RoleMenus.RoleMenuManageSelectedViewModel>>>
				(url: url, query: query);

			return result;
		}
	}
}
