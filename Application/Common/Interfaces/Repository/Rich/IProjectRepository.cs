using Domain.Rich.Aggregates.Projects;
using Domain.Rich.Aggregates.Projects.ValueObjects;

namespace Application.Common.Interfaces.Repository.Rich
{
    public interface IProjectRepository : Dtat.Ddd.IRepository<Project>
    {
        Task<bool> FindAnyByNameAsync(Title title, CancellationToken cancellationToken = default);
    }
}
