using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IRegulationGroupRepository : IRepository<RegulationGroup> 
    {
        Task<Result<bool>> IsUnique(RegulationGroup model,CancellationToken cancellationToken);
    }
}
