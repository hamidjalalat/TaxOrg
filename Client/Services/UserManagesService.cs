
namespace Client.Services
{
    public class UserManagesService : Infrastructure.ServiceBase
    {
        // ********************
        #region Properties

        private string strServiceUri = string.Empty;

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly Client.Services.Tokens.ITokensService _tokenService;
        private readonly Infrastructure.NazmAuthenticationStateProvider _authStateProvider;

        // ********************
        #endregion

        public UserManagesService
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

            strServiceUri = $"{Infrastructure.Utility.setServiceUri<UserManagesService>()}";
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <ViewModels.PagingViewModel
            <ViewModels.Users.UserManageViewModel>>>
            FetchAsync(ViewModels.Shared.PublicViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchAll";

            var result =
                await
                PostAsync
                <ViewModels.Shared.PublicViewModel,
                Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Users.UserManageViewModel>>>
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
            <ViewModels.Users.UserManageCreateViewModel>>>
            CreateUserAsync(ViewModels.Users.UserManageCreateViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}CreateUser";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageCreateViewModel,
                Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Users.UserManageCreateViewModel>>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
           System.Threading.Tasks.Task
           <ViewModels.Users.UserManageViewModel>
           FindByIdAsync(System.Guid Id)
        {
            string url = $"{strServiceUri}FindById";

            var result =
                await
                FindByIdAsync
                <Nazm.Results.Result
                <ViewModels.Users.UserManageViewModel>>
                (url: url, Id);

            return result.Value;
        }

		public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <ViewModels.Users.UserManageUpdateViewModel>>
			EditUserAsync(ViewModels.Users.UserManageUpdateViewModel viewModel)
		{
			try
			{
				string url = $"{strServiceUri}EditUser";

				var result =
					await
					PutAsync
					<ViewModels.Users.UserManageUpdateViewModel,
					Nazm.Results.Result<ViewModels.Users.UserManageUpdateViewModel>>
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
            <bool>>
            ChangePasswordAsync(ViewModels.Users.UserManageChangePasswordViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}ChangePassword";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageChangePasswordViewModel,
                Nazm.Results.Result<bool>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <bool>>
			ChangePasswordByAdminAsync(ViewModels.Users.UserManageChangePasswordByAdminViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}ChangePasswordByAdmin";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageChangePasswordByAdminViewModel,
                Nazm.Results.Result<bool>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <bool>>
            ActiveByAdminAsync(ViewModels.Users.UserManageActiveByAdminViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}ActiveByAdmin";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageActiveByAdminViewModel,
                Nazm.Results.Result<bool>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        } 

		public async
           System.Threading.Tasks.Task
           <ViewModels.Users.UserManageViewModel>
           FetchUserRolesAsync(int Id)
        {
            string url = $"{strServiceUri}FetchUserRoles?id=${Id}";

            var result =
                await
                GetAsync
                <Nazm.Results.Result
                <ViewModels.Users.UserManageViewModel>>
                (url: url);

            return result.Value;
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <ViewModels.PagingViewModel
            <ViewModels.Users.UserManageStatisticReportViewModel>>>
            FetchStatisticReportAsync(ViewModels.Users.UserManageStatisticReportInputParamsViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchStatisticReport";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageStatisticReportInputParamsViewModel,
                Nazm.Results.Result<ViewModels.PagingViewModel<ViewModels.Users.UserManageStatisticReportViewModel>>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <byte[]>>
            FetchStatisticReportAndFileDownloadAsync(ViewModels.Users.UserManageStatisticReportInputParamsViewModel inputParamsViewModel)
        {
            string url = $"{strServiceUri}FetchStatisticReport";

            var result =
                await
                PostAsync
                <ViewModels.Users.UserManageStatisticReportInputParamsViewModel,
                Nazm.Results.Result<byte[]>>
                (url: url, viewModel: inputParamsViewModel);

            return result;
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <bool>>
            DeleteAsync(System.Guid Id)
		{
			string url = $"{strServiceUri}";

			var result =
				await
				DeleteAsync<Nazm.Results.Result<bool>>
                (url: url, Id);

			return result;

		}

	}
}
