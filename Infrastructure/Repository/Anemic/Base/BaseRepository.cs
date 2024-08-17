using Application.Common.Interfaces.Repository.Anemic.Base;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;

namespace Infrastructure.Repository.Anemic.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fields

        private readonly EFContext _context;
        private readonly OracleContext _orclContext;
        private DbSet<T> _entities;

        #endregion

        #region Constructor

        public BaseRepository(EFContext context) => _context = context;
        public BaseRepository(OracleContext orclContext) => _orclContext = orclContext;
        #endregion

        #region Private Properties

        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    if (_context != null)
                        _entities = _context.Set<T>();
                    else if (_orclContext != null)
                        _entities = _orclContext.Set<T>();
                }

                return _entities;
            }
        }

        #endregion

        #region Implementation of IRepository

        public async Task<T?> FindByIdAsync(object ID,CancellationToken cancellationToken)
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
