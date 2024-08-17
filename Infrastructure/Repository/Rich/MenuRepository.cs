using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Rich.Aggregates.Menus;
using Domain.Rich.Aggregates.Menus.ValueObjects;
using Domain.Rich.Aggregates;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Rich;
using Dtat.Ddd.EntityFrameworkCore.Rich;
using Application.Common.Interfaces.Repository.Rich;

namespace Infrastructure.Repository.Rich
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(DatabaseContext databaseContext) : base(databaseContext: databaseContext)
        {
        }

        public async
            Task
            <bool> FindAnyByNameAsync
            (Title title,
             CancellationToken cancellationToken = default)
        {
            bool result =
                await
                DbSet
                .Where(current => current.Title == title)
                .AnyAsync(cancellationToken: cancellationToken);

            return result;
        }

        public override IQueryable<Menu> GetAll()
        {
            return base.GetAll();
        }


    }
}
