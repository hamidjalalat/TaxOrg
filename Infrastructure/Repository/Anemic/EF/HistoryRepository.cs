using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Infrastructure.Repository.Anemic.Base;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class HistoryRepository : BaseRepository<EntityAutoHistory>, IHistoryRepository
    {
        public HistoryRepository(EFContext context) : base(context)
        {
        }
    }
}
