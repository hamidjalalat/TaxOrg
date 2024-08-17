using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic.Entities;


namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IHistoryRepository : IBaseRepository<EntityAutoHistory> { }
}
