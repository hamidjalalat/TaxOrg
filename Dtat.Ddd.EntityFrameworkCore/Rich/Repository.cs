using System.Linq;
using Domain.Rich.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Dtat.Ddd.EntityFrameworkCore.Rich
{
    public abstract class Repository<TEntity> :
        IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        public Repository
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
            System.Threading.Tasks.Task AddAsync
            (TEntity entity,
            System.Threading.CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await DbSet.AddAsync
                (entity: entity, cancellationToken: cancellationToken);
        }

        public
            virtual
            async
            System.Threading.Tasks.Task AddRangeAsync
            (System.Collections.Generic.IEnumerable<TEntity> entities,
            System.Threading.CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entities));
            }

            await DbSet.AddRangeAsync
                (entities: entities, cancellationToken: cancellationToken);
        }

        public
            virtual
            async
            System.Threading.Tasks.Task RemoveAsync
            (TEntity entity, System.Threading.CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                DbSet.Remove(entity: entity);
            }, cancellationToken: cancellationToken);
        }


        public
            virtual
            async
            System.Threading.Tasks.Task SoftRemoveAsync
            (TEntity entity, System.Threading.CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                entity.IsDeleted = true;
                DatabaseContext.Entry(entity).State = EntityState.Modified;

            }, cancellationToken: cancellationToken);
        }

        public
            virtual
            async
            System.Threading.Tasks.Task<bool> RemoveByIdAsync
            (System.Guid id, System.Threading.CancellationToken cancellationToken = default)
        {
            TEntity entity =
                await GetByIdAsync(id: id, cancellationToken: cancellationToken);

            if (entity == null)
            {
                return false;
            }

            await RemoveAsync
                (entity: entity, cancellationToken: cancellationToken);

            return true;
        }

        public
            virtual
            async
            System.Threading.Tasks.Task RemoveRangeAsync
            (System.Collections.Generic.IEnumerable<TEntity> entities,
            System.Threading.CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entities));
            }

            foreach (var entity in entities)
            {
                await RemoveAsync
                    (entity: entity, cancellationToken: cancellationToken);
            }
        }

        public
            virtual
            async
            System.Threading.Tasks.Task UpdateAsync
            (TEntity entity, System.Threading.CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var attachedEntity =
                    DatabaseContext.Attach(entity: entity);

                if (attachedEntity.State != EntityState.Modified)
                {
                    attachedEntity.State =
                        EntityState.Modified;
                }
            }, cancellationToken: cancellationToken);
        }

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

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
    }
}
