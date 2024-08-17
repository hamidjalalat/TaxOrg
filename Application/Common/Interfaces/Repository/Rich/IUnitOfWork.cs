using Domain.Rich.Aggregates;

namespace Application.Common.Interfaces.Repository.Rich
{
    public interface IUnitOfWork : IDisposable
    {

        IProjectRepository Projects { get; }
        IMenuRepository Menus { get; }

        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task Commit();
    }
}
