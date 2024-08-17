using Application.Common.Interfaces.Repository.Anemic.Oracle;
using Domain.Anemic.Entities;
using Infrastructure.Repository.Anemic.Base;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.Oracle
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(OracleContext context) : base(context)
        {
        }
    }
}
