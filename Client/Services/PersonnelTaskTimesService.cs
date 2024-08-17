using System.Net.Http.Json;

namespace Client.Services
{
    public class PersonnelTaskTimesService : Infrastructure.ServiceBase
    {
        string strServiceUri = string.Empty;

        public PersonnelTaskTimesService
            (System.Net.Http.HttpClient http, LogsService logsService) : base(http, logsService)
        {
            BaseUrl = Infrastructure.Pages.Utility.getBaseUrl();

            strServiceUri = $"{ Infrastructure.Pages.Utility.setServiceUri<PersonnelTaskTimesService>() }";
        }

        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <System.Collections.Generic.IList<ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>>
            GetAsync()
        {
            string url = $"{ strServiceUri }getall";

            var result =
                await
                GetAsync
                <Nazm.Results.Result
                <System.Collections.Generic.IList<ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>>
                (url: url);

            return result;
        }

        public async
           System.Threading.Tasks.Task
           <ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>
           GetByIdAsync(System.Guid Id)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                GetByIdAsync
                <Nazm.Results.Result
                <ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>
                (url: url, Id);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          <Nazm.Results.Result
          <ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>
          PostAsync(ViewModels.PersonnelTaskTimes.CreateRequestViewModel viewModel)
        {
            try
            {
                string url = $"{ strServiceUri }";

                var result =
                    await
                    PostAsync
                    <ViewModels.PersonnelTaskTimes.CreateRequestViewModel,
                    Nazm.Results.Result<ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>
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
          <ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>
          PutAsync(ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel viewModel)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                PutAsync
                <ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel, Nazm.Results.Result<ViewModels.PersonnelTaskTimes.PersonnelTaskTimeViewModel>>
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
                DeleteAsync<FluentResults.Result>
                (url: url, Id);

            //return;
        }

    }
}
