using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dtat.Ddd.EntityFrameworkCore.Rich
{
    public abstract class RepositoryQuery<TEntity> :
        IQueryRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        public RepositoryQuery
            (DbContext databaseContext) : base()
        {
            DatabaseContext =
                databaseContext ??
                throw new System.ArgumentNullException(paramName: nameof(databaseContext));

            DbSet =
                DatabaseContext.Set<TEntity>();
        }

        // **********
        protected DbSet<TEntity> DbSet { get; }
        // **********

        // **********
        protected DbContext DatabaseContext { get; }
        // **********


        public
            virtual
            async
            System.Threading.Tasks.Task
            <System.Collections.Generic.IEnumerable<TEntity>> GetAllAsync
            (System.Threading.CancellationToken cancellationToken = default)
        {
            // ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
            var result =
                await
                DbSet.ToListAsync(cancellationToken: cancellationToken)
                ;

            return result;
        }

        public
            virtual
            async
            System.Threading.Tasks.Task<TEntity> GetByIdAsync
            (System.Guid id, System.Threading.CancellationToken cancellationToken = default)
        {
            var result =
                await DbSet.FindAsync(keyValues: new object[] { id },
                cancellationToken: cancellationToken);

            return result;
        }

        public
            virtual
            async
            System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetSomeAsync
            (System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate,
            System.Threading.CancellationToken cancellationToken = default)
        {
            // ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
            var result =
                await
                DbSet
                .Where(predicate: predicate)
                .ToListAsync(cancellationToken: cancellationToken)
                ;

            return result;
        }
    }
}
