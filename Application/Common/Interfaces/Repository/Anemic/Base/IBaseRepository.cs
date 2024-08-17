using Dapper;

namespace Application.Common.Interfaces.Repository.Anemic.Base
{
    public interface IBaseRepository<T>
    {
        Task<T?> FindByIdAsync(object ID, CancellationToken cancellationToken);
        IQueryable<T> GetAll { get; }

    }
}
