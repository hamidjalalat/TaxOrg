using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.Dapper
{
    public class UserDapperRepository : DapperRepository<User>, IUserDapperRepository
    {
        public UserDapperRepository(DapperContext context) : base(context)
        {
        }
       
    }
}
