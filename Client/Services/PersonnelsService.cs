using System.Net.Http.Json;

namespace Client.Services
{
    public class PersonnelsService : Infrastructure.ServiceBase
    {
        string strServiceUri = string.Empty;

        public PersonnelsService
            (System.Net.Http.HttpClient http, LogsService logsService) : base(http, logsService)
        {
            BaseUrl = Infrastructure.Pages.Utility.getBaseUrl();

            strServiceUri = $"{ Infrastructure.Pages.Utility.setServiceUri<PersonnelsService>() }";
        }

        public async
            System.Threading.Tasks.Task
            //<FluentResults.Result
            //<Dtat.Results.Result
            <Nazm.Results.Result
            //<System.Collections.Generic.IList<Domain.Aggregates.Personnels.Personnel>>>
            <System.Collections.Generic.IList<ViewModels.Personnels.PersonnelViewModel>>>
            GetAsync()
        {
            //string url = $"{ Infrastructure.Pages.ConstPages.Personnels }/getall";
            string url = $"{ strServiceUri }getall";

            var result =
                await
                GetAsync
                //<FluentResults.Result
                //<Dtat.Results.Result
                <Nazm.Results.Result
                //<System.Collections.Generic.IList<Domain.Aggregates.Personnels.Personnel>>>
                <System.Collections.Generic.IList<ViewModels.Personnels.PersonnelViewModel>>>
                (url: url);

            return result;
        }

        public async
           System.Threading.Tasks.Task
           //<Dtat.Results.Result
           <ViewModels.Personnels.PersonnelViewModel>
           GetByIdAsync(System.Guid Id)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                GetByIdAsync
                //<Dtat.Results.Result
                <Nazm.Results.Result
                <ViewModels.Personnels.PersonnelViewModel>>
                (url: url, Id);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          //<Dtat.Results.Result
          <Nazm.Results.Result
          <ViewModels.Personnels.PersonnelViewModel>>
          PostAsync(ViewModels.Personnels.CreateRequestViewModel viewModel)
        {
            try
            {
                //string url = "personnels/PostAsync";
                string url = $"{ strServiceUri }";

                var result =
                    await
                    PostAsync
                    <ViewModels.Personnels.CreateRequestViewModel,
                    //Dtat.Results.Result<ViewModels.Personnels.PersonnelViewModel>>
                    Nazm.Results.Result<ViewModels.Personnels.PersonnelViewModel>>
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
          //<Dtat.Results.Result
          <ViewModels.Personnels.PersonnelViewModel>
          PutAsync(ViewModels.Personnels.PersonnelViewModel viewModel)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                PutAsync
                //<ViewModels.Personnels.PersonnelViewModel, Dtat.Results.Result<ViewModels.Personnels.PersonnelViewModel>>
                <ViewModels.Personnels.PersonnelViewModel, Nazm.Results.Result<ViewModels.Personnels.PersonnelViewModel>>
                (url: url, viewModel);

            return result.Value;
        }

        public async
          System.Threading.Tasks.Task
          //<Dtat.Results.Result
          DeleteAsync(System.Guid Id)
        {
            string url = $"{ strServiceUri }";

            var result =
                await
                //DeleteAsync<Dtat.Results.Result>
                DeleteAsync<FluentResults.Result>
                (url: url, Id);

            //return;
        }


        public async
            System.Threading.Tasks.Task
            <Nazm.Results.Result
            <System.Collections.Generic.IList<ViewModels.Personnels.PersonnelViewModel>>>
            GetQueryAsync()
        {
            //string url = $"{ Infrastructure.Pages.ConstPages.Personnels }/getall";
            string url = $"{strServiceUri}getallQuery";

            var result =
                await
                GetAsync
                <Nazm.Results.Result
                <System.Collections.Generic.IList<ViewModels.Personnels.PersonnelViewModel>>>
                (url: url);

            return result;
        }

    }
}
