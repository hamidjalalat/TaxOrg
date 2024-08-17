using Application.Common.Interfaces.Repository.Anemic.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.EF
{
    public class EFBaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fields

        private readonly EFContext _context;
        private DbSet<T> _entities;

        #endregion

        #region Constructor

        public EFBaseRepository(EFContext context) => _context = context;

        #endregion

        #region Private Properties

        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }

                return _entities;
            }
        }

        #endregion

        #region Implementation of IRepository

        public async Task<T?> FindByIdAsync(object ID, CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(new object[] { ID }, cancellationToken);
        }

        public IQueryable<T> GetAll
        {
            get
            {
                return Entities;
            }
        }

        #endregion

    }
}
