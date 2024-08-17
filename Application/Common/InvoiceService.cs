using Application.Common.Models;
using ViewModels.Invoices;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Common
{
    public class InvoiceService:BaseService
    {
        public InvoiceService(HttpClient client) : base(client)
        {
        }

        protected override string GetApiUrl()
        {
            return "api/invoices/async";
        }

        public async Task<IList<InvoiceViewModel>> GetAsync()
        {
            var result = await GetAsync<IList<InvoiceViewModel>>();

            return result;
        }

        public async Task<List<InvoiceViewModel>> PostAsync(List<InvoiceBodeyViewModel> viewModel,string token,string xOrgId)
        {
            var result = await PostAsync<List<InvoiceBodeyViewModel>, List<InvoiceViewModel>>(viewModel, token, xOrgId);
         
            return result;
        }

    }
}
