namespace Client.Services
{
    public class TasksService : Infrastructure.ServiceBase
    {
        string strServiceUri = string.Empty;

        public TasksService
            (System.Net.Http.HttpClient http, LogsService logsService) : base(http, logsService)
        {
            BaseUrl = Infrastructure.Pages.Utility.getBaseUrl();

            strServiceUri = $"{Infrastructure.Pages.Utility.setServiceUri<TasksService>()}";
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <System.Collections.Generic.IList<ViewModels.Tasks.TaskViewModel>>>
            GetAsync()
        {
            string url = $"{strServiceUri}getall";

            var result =
                await
                GetAsync
                <Nazm.Results.Result
                <System.Collections.Generic.IList<ViewModels.Tasks.TaskViewModel>>>
                (url: url);

            return result;
        }

        public async
           System.Threading.Tasks.Task
           <ViewModels.Tasks.TaskViewModel>
           GetByIdAsync(System.Guid Id)
        {
            string url = $"{strServiceUri}";

            var result =
                await
                GetByIdAsync
                <Nazm.Results.Result
                <ViewModels.Tasks.TaskViewModel>>
                (url: url, Id);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result
          <ViewModels.Tasks.TaskViewModel>>
          PostAsync(ViewModels.Tasks.CreateRequestViewModel viewModel)
        {
            try
            {
                string url = $"{strServiceUri}";

                var result =
                    await
                    PostAsync
                    <ViewModels.Tasks.CreateRequestViewModel,
                    Nazm.Results.Result<ViewModels.Tasks.TaskViewModel>>
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
          <ViewModels.Tasks.TaskViewModel>
          PutAsync(ViewModels.Tasks.TaskViewModel viewModel)
        {
            string url = $"{strServiceUri}";

            var result =
                await
                PutAsync
                <ViewModels.Tasks.TaskViewModel, Nazm.Results.Result<ViewModels.Tasks.TaskViewModel>>
                (url: url, viewModel);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          DeleteAsync(System.Guid Id)
        {
            string url = $"{strServiceUri}";

            var result =
                await
                DeleteAsync<FluentResults.Result>
                (url: url, Id);

            //return;
        }


        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <System.Collections.Generic.IList<ViewModels.Tasks.TaskViewModel>>>
            GetQueryAsync()
        {
            string url = $"{strServiceUri}getallQuery";

            var result =
                await
                GetAsync
                <Nazm.Results.Result
                <System.Collections.Generic.IList<ViewModels.Tasks.TaskViewModel>>>
                (url: url);

            return result;
        }

    }
}
