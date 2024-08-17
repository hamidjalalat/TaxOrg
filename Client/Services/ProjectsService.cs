namespace Client.Services
{
	public class ProjectsService : Infrastructure.ServiceBase
	{
        string strServiceUri = string.Empty;

        public ProjectsService
			(System.Net.Http.HttpClient http, LogsService logsService) : base(http, logsService)
		{
            BaseUrl = Infrastructure.Pages.Utility.getBaseUrl();

            strServiceUri = $"{ Infrastructure.Pages.Utility.setServiceUri<ProjectsService>() }";
        }

		public
			async
			System.Threading.Tasks.Task
            <Nazm.Results.Result
            <System.Collections.Generic.IList<ViewModels.Projects.ProjectViewModel>>>
			GetAsync()
		{
            string url = $"{ strServiceUri }getall";

            var result =
				await
				GetAsync
                <Nazm.Results.Result
                <System.Collections.Generic.IList<ViewModels.Projects.ProjectViewModel>>>
				(url: url);

			return result;
		}

        public async
           System.Threading.Tasks.Task
           <ViewModels.Projects.ProjectViewModel>
           GetByIdAsync(System.Guid Id)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                GetByIdAsync
                <Nazm.Results.Result
                <ViewModels.Projects.ProjectViewModel>>
                (url: url, Id);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          <ViewModels.Projects.ProjectViewModel>
          PostAsync(ViewModels.Projects.CreateRequestViewModel viewModel)
        {
            try
            {
                string url = $"{ strServiceUri }";

                var result =
                    await
                    PostAsync
                    <ViewModels.Projects.CreateRequestViewModel, Nazm.Results.Result<ViewModels.Projects.ProjectViewModel>>
                    (url: url, viewModel);

                return result.Value;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            return default;
        }

        public async
          System.Threading.Tasks.Task
          <ViewModels.Projects.ProjectViewModel>
          PutAsync(ViewModels.Projects.ProjectViewModel viewModel)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                PutAsync
                <ViewModels.Projects.ProjectViewModel, Nazm.Results.Result<ViewModels.Projects.ProjectViewModel>>
                (url: url, viewModel);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          DeleteAsync(System.Guid Id)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                DeleteAsync<Nazm.Results.Result>
                (url: url, Id);

            //return;
        }


    }
}
