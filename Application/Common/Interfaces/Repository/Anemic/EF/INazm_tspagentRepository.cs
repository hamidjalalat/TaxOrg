using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using ViewModels.Invoices;
using ViewModels.Nazm_tspagents;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface INazm_tspagentRepository : IRepository<Nazm_tspagent> 
    {
        Task<Result<bool>> IsUnique(Nazm_tspagent model,CancellationToken cancellationToken);
        Task<InquiryFindByInnoViewModel> FindByInnoAsync(long inno);
        Task<InquiryFindByInnoViewModel> FindByInnoForCancelInvoiceAsync(long inno);
        Task<InquiryFindByInnoViewModel> FindByInnoForEditInvoiceAsync(long inno);
        Task<InquiryFindByRefrenceViewModel> FindByReferenceIdAsync(string inno);
        Task<List<string>> GetAllByFilterForInQuery(int take);
        Task<List<InvoiceSelectViewModel>> GetAllByFilterForInvoice(int take);
        Task<List<InvoiceSelectViewModel>> GetAllByFilterForCancelInvoice(int take);
        Task<List<InvoiceSelectViewModel>> GetAllByFilterForEditInvoice(int take);
        Task<long> GetCountInvoiceSending(Nazm_tspagentInputDateInvoiceViewModel date);
        Task<long> GetCountInvoiceCancel(Nazm_tspagentInputDateInvoiceViewModel date);
        Task<long> GetCountInvoiceFailed(Nazm_tspagentInputDateInvoiceViewModel date);
        Task<long> GetCountInvoicePending(Nazm_tspagentInputDateInvoiceViewModel date);
        Task<long> GetCountInvoiceSuccess(Nazm_tspagentInputDateInvoiceViewModel date);
        Task<long> GetCountInvoice(Nazm_tspagentInputDateInvoiceViewModel date);
        IQueryable<Nazm_tspagent> GetAllByFilter(Nazm_tspagentInputParamsViewModel InputParams);
    }
}
