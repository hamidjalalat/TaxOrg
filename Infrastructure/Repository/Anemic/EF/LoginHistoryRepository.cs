using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class LoginHistoryRepository : EFRepository<LoginHistory>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(EFContext context) : base(context)
        {
        }

    }


}
