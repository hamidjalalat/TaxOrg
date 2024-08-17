using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface INetsaleRepository : IRepository<Netsale> 
    {
        Task<Result<bool>> IsUnique(Netsale model,CancellationToken cancellationToken);
    }
}
