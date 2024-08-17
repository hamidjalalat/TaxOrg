using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.Dapper
{
    public class ProductDapperRepository : DapperRepository<Product>, IProductDapperRepository
    {
        public ProductDapperRepository(DapperContext context) : base(context)
        {
        }

    }
}
