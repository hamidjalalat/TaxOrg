using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IInvoiceModelRepository : IRepository<InvoiceModel> 
    {
        Task<Result<bool>> IsUnique(InvoiceModel model,CancellationToken cancellationToken);
    }
}
