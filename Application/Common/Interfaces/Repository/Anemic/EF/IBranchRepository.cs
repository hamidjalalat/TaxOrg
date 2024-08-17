using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IBranchRepository : IRepository<Branch> 
    {
        Task<Result<bool>> IsUnique(Branch model,CancellationToken cancellationToken);
    }
}
