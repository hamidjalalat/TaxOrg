using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface ITspagentRepository : IRepository<Tspagent> 
    {
        Task<Result<bool>> IsUnique(Tspagent model,CancellationToken cancellationToken);
    }
}
