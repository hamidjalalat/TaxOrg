using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IDimProductRepository : IRepository<DimProduct> 
    {
        Task<Result<bool>> IsUnique(DimProduct model,CancellationToken cancellationToken);
    }
}
