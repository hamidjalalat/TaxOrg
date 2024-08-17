using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.Anemic.Contexts
{
    public class OracleContext : DbContext
    {
        public OracleContext(DbContextOptions<OracleContext> options) : base(options: options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
