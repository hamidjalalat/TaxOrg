using Application.Common.Models;
using ViewModels.Invoices;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Common
{
    public class InquiryService:BaseService
    {
        public InquiryService(HttpClient client) : base(client)
        {
        }

        protected override string GetApiUrl()
        {
            return "api/inquiry/byReferenceId";
        }

        public async Task<List<InquiryViewModel>> PostAsync(List<string> viewModel,string token,string xOrgId)
        {
            var result = await PostAsync<List<string> , List<InquiryViewModel>>(viewModel, token, xOrgId);
         
            return result;
        }

    }
}
