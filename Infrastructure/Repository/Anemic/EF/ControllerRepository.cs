using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class ControllerRepository : EFRepository<Controller>, IControllerRepository
    {
        public ControllerRepository(EFContext context) : base(context)
        {
        }
    }
}
