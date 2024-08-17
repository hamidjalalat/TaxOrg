
using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Anemic.EF
{
    public class EFRepository<T> : IRepository<T> where T : class, ISoftDeletable
    {
        #region Fields

        private readonly EFContext _context;
        private DbSet<T> _entities;

        #endregion

        #region Constructor

        public EFRepository(EFContext context) => _context = context;

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

        public async Task<T?> FindByIdAsync(object ID,CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(new object[] { ID }, cancellationToken);
        }

        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void UpdateSpecificField(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            foreach (var property in updatedProperties)
            {
                _context.Entry(entity).Property(property).IsModified = true;
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;
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
