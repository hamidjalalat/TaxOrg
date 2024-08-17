using Domain.Rich.Aggregates.Menus;
using Domain.Rich.Aggregates.Menus.ValueObjects;

namespace Application.Common.Interfaces.Repository.Rich
{
    public interface IMenuRepository : Dtat.Ddd.IRepository<Menu>
    {
        Task<bool> FindAnyByNameAsync(Title title, CancellationToken cancellationToken = default);

        IQueryable<Menu> GetAll();

    }
}
